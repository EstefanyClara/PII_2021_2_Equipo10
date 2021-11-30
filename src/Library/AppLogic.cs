using System;
using System.Collections;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Proyect
{
    /// <summary>
    /// Clase singleton para guardar los datos de la Aplicacion.
    /// Solo hay una instancia de esta clase, y es la que colabora con todas las demas.
    /// Esta clase guarda las instancias tanto de campanias y de emprendedores, asi como de los rubros, clasificaciones, y habilitaciones (La usa de manera muy cercana).
    /// Todos lso comabios qu ese hagan a algun objeto, pasan por esta clase, esto permite que sea mas facilemnete el utilizar la logica del programa, sin depender de los handlers.
    /// </summary>
    public sealed class AppLogic
    {
        private LocationApiClient client = APILocationContainer.Instance.APIdeLocalizacion;
        private readonly static AppLogic _instance = new AppLogic();
        private IList<Company> companies;
        private IList<Emprendedor> entrepreneurs;
        private List<Rubro> validRubros = new List<Rubro>();
        private List<Qualifications> validQualifications = new List<Qualifications>();

        private List<Classification> validClasification = new List<Classification>();

        /// <summary>
        /// Obtiene las companias que estan registradas.
        /// </summary>
        /// <value>companies.</value>
        public IList<Company> Companies
        {
            get{return this.companies;}
            set{this.companies = value;}
        }

        /// <summary>
        /// Obtiene los emprendedores que estan registrados.
        /// </summary>
        /// <value>entrepreneurs.</value>
        public IList<Emprendedor> Entrepreneurs
        {
            get{return this.entrepreneurs;}
            set{this.entrepreneurs = value;}
        }

        /// <summary>
        /// Obtiene los rubros habilitados.
        /// </summary>
        /// <value>validRubros.</value>
        public List<Rubro> Rubros
        {
            get{return DeserializeRubros();}
        }

        /// <summary>
        /// Obtiene la lista de habilitaciones registradas.
        /// </summary>
        /// <value>validQualifications.</value>
        public List<Qualifications> Qualifications
        {
            get{return DeserializeQualifications();}
        }

        /// <summary>
        /// Obtiene la lista de clasificaciones/categorias registradas para los productos.
        /// </summary>
        /// <value>validClasification.</value>
        public List<Classification> Classifications
        {
            get{return DeserializeClasification();}
        }

        /// <summary>
        /// Convierte a Json la lista de rubros (así cuando se quiera agregar uno nuevo, no hay que detener el programa).
        /// Este metodo lo posee AppLogic, porque es la que tiene la lista (es la experta en la informacion). 
        /// Este es un metodo polimorfico.
        /// </summary>
        /// <param name="rubro">La lista de rubros antes de serializar.</param>
        /// <returns>La lisat de rubros serializada.</returns>
        public string ConvertToJson(List<Rubro> rubro)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };

            return System.Text.Json.JsonSerializer.Serialize(rubro, options);
        }

        /// <summary>
        /// Convierte a Json la lista de habiliatciones (así cuando se quiera agregar una nueva, no hay que detener el programa).
        /// Este metodo lo posee AppLogic, porque es la que tiene la lista (es la experta en la informacion).
        /// Este es un metodo polimorfico.
        /// </summary>
        /// <param name="habilitaciones">La lista de habiliatciones antes de serializarla.</param>
        /// <returns>La lisat de habiliatciones serializada.</returns>
        public string ConvertToJson(List<Qualifications> habilitaciones)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            return System.Text.Json.JsonSerializer.Serialize(habilitaciones, options);
        }

        /// <summary>
        /// Convierte a Json la lista de calsificaciones (así cuando se quiera agregar una nueva, no hay que detener el programa).
        /// Este metodo lo posee AppLogic, porque es la que tiene la lista (es la experta en la informacion).
        /// Este es un metodo polimorfico.
        /// </summary>
        /// <param name="clasificaciones">La lisat de clasificaiones nates de serializar.</param>
        /// <returns>La lisat de clasificaciones serializada.</returns>
        public string ConvertToJson(List<Classification> clasificaciones)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };

            return System.Text.Json.JsonSerializer.Serialize(clasificaciones, options);
        }

        /// <summary>
        /// Convierte a Json la lista de emprendedores (así cuando se detenga el programa, no se pierda la informacion).
        /// Este metodo lo posee AppLogic, porque es la que tiene la lista (es la experta en la informacion).
        /// Este es un metodo polimorfico. 
        /// </summary>
        /// <param name="emprendedores">La lisat de meprendedores antes de serializar.</param>
        /// <returns>La lisat de emprendedores serializada.</returns>
        public string ConvertToJson(IList<Emprendedor> emprendedores)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            foreach(Emprendedor value in emprendedores)
            {
                foreach(IOffer item in value.PurchasedOffers)
                    {
                            if(item.GetType().Equals(typeof(ConstantOffer)))
                            {
                                    value.OfertasConstantes.Add(item as ConstantOffer);
                            }else
                            {
                                    value.OfertasNoConstantes.Add(item as NonConstantOffer);
                        }
                    }
                    value.PurchasedOffers.Clear();
            }
            return System.Text.Json.JsonSerializer.Serialize(emprendedores, options);
        }

        /// <summary>
        /// Convierte a Json la lista de companias (así cuando se detenga el programa, no se pierda la informacion).
        /// Este metodo lo posee AppLogic, porque es la que tiene la lista (es la experta en la informacion).
        /// Este es un metodo polimorfico. 
        /// </summary>
        /// <param name="companies">La lisat de companias antes de serilizar.</param>
        /// <returns>La lista de compnais serializada.</returns>
        public string ConvertToJson(IList<Company> companies)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            foreach(Company value in companies)
            {
                foreach(IOffer item in value.OffersPublished)
                    {
                            if(item.GetType().Equals(typeof(ConstantOffer)))
                            {
                                    value.OfertasConstantes.Add(item as ConstantOffer);
                            }else
                            {
                                    value.OfertasNoConstantes.Add(item as NonConstantOffer);
                        }
                    }
                    value.OffersPublished.Clear();
            }
            return System.Text.Json.JsonSerializer.Serialize(companies, options);
        }

        /// <summary>
        /// Deserializa la listas de rubros del archivo json.
        /// AppLogic tiene la responsabilidad, porque es la que tiene la logica del programa y la que usa la lista (por expert).
        /// </summary>
        /// <returns>La lista de rubros deserializada.</returns>
        public List<Rubro> DeserializeRubros()
        {
                string json = System.IO.File.ReadAllText(@"../Library/Persistencia/Rubros.json");
                return JsonConvert.DeserializeObject<List<Rubro>>(json);
        }

        /// <summary>
        /// Deserializa la lista de habilitaciones del archivo json.
        /// AppLogic tiene la responsabilidad, porque es la que tiene la logica del programa y la que usa la lista (por expert).
        /// </summary>
        /// <returns>La lista de habiliatciones deserializada.</returns>
        public List<Qualifications> DeserializeQualifications()
        {
                string json = System.IO.File.ReadAllText(@"../Library/Persistencia/Habilitaciones.json");
                return JsonConvert.DeserializeObject<List<Qualifications>>(json);
        }

        /// <summary>
        /// Deserializa la lista de clasificaciones del archivo json.
        /// AppLogic tiene la responsabilidad, porque es la que tiene la logica del programa y la que usa la lista (por expert).
        /// </summary>
        /// <returns>La lista de clasificaciones deserializada.</returns>
        public List<Classification> DeserializeClasification()
        {
                string json = System.IO.File.ReadAllText(@"../Library/Persistencia/ClasificacionesProductos.json");
                return JsonConvert.DeserializeObject<List<Classification>>(json);
        }

        /// <summary>
        /// Deserializa la lista de emprendedores del archivo json.
        /// AppLogic tiene la responsabilidad, porque es la que tiene la logica del programa y la que usa la lista (por expert).
        /// </summary>
        /// <returns>La lisat de emprendedores deserializada.</returns>
        public IList<Emprendedor> DeserializeEntrenprenuers()
        {
                string json = System.IO.File.ReadAllText(@"../Library/Persistencia/Emprendedores.json");
                IList<Emprendedor> emprendedores = JsonConvert.DeserializeObject<IList<Emprendedor>>(json);
                foreach(Emprendedor item in emprendedores)
                {
                    foreach(IOffer value in item.OfertasConstantes)
                    {
                        item.PurchasedOffers.Add(value);
                    }
                    foreach(IOffer value in item.OfertasNoConstantes)
                    {
                        item.PurchasedOffers.Add(value);
                    }
                    item.OfertasConstantes.Clear();
                    item.OfertasNoConstantes.Clear();
                }
                return emprendedores;
        }

        /// <summary>
        /// Deserializa la listas de companias del archivo json.
        /// AppLogic tiene la responsabilidad, porque es la que tiene la logica del programa y la que usa la lista (por expert).
        /// </summary>
        /// <returns>La lista de emprendedores deserializada.</returns>
        public IList<Company> DeserializeCompanies()
        {
            string json = System.IO.File.ReadAllText(@"../Library/Persistencia/Companias.json");
            IList<Company> companias = JsonConvert.DeserializeObject<IList<Company>>(json);
            foreach(Company item in companias)
            {
                foreach(IOffer value in item.OfertasConstantes)
                {
                    item.OffersPublished.Add(value);
                }
                foreach(IOffer value in item.OfertasNoConstantes)
                {
                    item.OffersPublished.Add(value);
                }
                item.OfertasConstantes.Clear();
                item.OfertasNoConstantes.Clear();
            }
            return companias;
        }

        private AppLogic()
        {
            this.companies = new List<Company>();
            this.entrepreneurs = new List<Emprendedor>();
        }

        /// <summary>
        /// Obtiene la instancia de AppLogic.
        /// </summary>
        /// <value>_instance.</value>
        public static AppLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        /// <summary>
        /// Registra a un id de usuario, como administrador.
        /// Le delega la responsabilidad a Administrator, la que tiene la lista donde se depositaran los id.
        /// </summary>
        /// <param name="user_id">El id del usuario.</param>
        /// <param name="confirmCode">El codigo de confirmacion (contraseña) para determinar si puede o no, ser administrador.</param>
        /// <returns>True si cumple con lo necesario para se administrador, false en caso contrario.</returns>
        public bool AddAdministrator(string user_id, string confirmCode)
        {
            return Administrator.Instance.AddAdministrator(confirmCode, user_id);
        }

        /// <summary>
        /// Obtiene le codigo que un usuario usara si se quiere registrar como compania.
        /// Le delega la responsabiliad a Administrator (La que tiene la lista de todos los codigos, y la que los maneja).
        /// </summary>
        /// <returns>El codigo que usara una compnai para registrarse.</returns>
        public string Invite()
        {
            return Administrator.Instance.Invite();
        }
        
        
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Emprendedor"/>.
        /// Utiliza el patron creator.
        /// </summary>
        /// <param name="name">El nombre del emprendedor.</param>
        /// <param name="ubication">La ubicacion del emprendedor.</param>
        /// <param name="rubro">El rubro del emprendedor.</param>
        /// <param name="habilitaciones">Las habilitaciones que tiene el emprendedor.</param>
        /// <param name="especializaciones">Las especializaciones que tiene el emprendedor.</param>
        /// <param name="user_Id">Id que tiene el emprendedor.</param> 
        /// <param name="user_Contact">Contacto del emprendedor.</param> 
        public void RegisterEntrepreneurs(string user_Id, string name, string ubication, Rubro rubro, List<Qualifications> habilitaciones, string especializaciones, string user_Contact)
        {
            try
            {
                entrepreneurs.Add(new Emprendedor(user_Id, name, ubication, rubro, user_Contact, habilitaciones, especializaciones));
            }
            catch (EmptyUserBuilderException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Registra a una compania
        /// Le delaga la responsabilidad de crear la compnai a Administrator, la que tiene que comprara el codigo necesario para la companias.
        /// Guarda a la compania en la lista de companias (Por expert).
        /// </summary>
        /// <param name="companyToken">El codigo que ingreso una compnai para registrase.</param>
        /// <param name="user_Id">El id de ususario.</param>
        /// <param name="name">El nombre de la compañia.</param>
        /// <param name="ubication">La ubicacion de la compania.</param>
        /// <param name="rubro">El rubro al que pertenece la compañia.</param>
        /// <param name="user_Contact">Contacto de la compania.</param>
        /// <returns>Mensaje de confirmacion.</returns>
        public bool RegistrarCompany(string companyToken, string user_Id, string name, string ubication, Rubro rubro, string user_Contact)
        {
            Company company = Administrator.Instance.ConfirmCompanyRegistration(companyToken, user_Id, name, ubication, rubro, user_Contact);
            if (company == null)
            {
                return false;
            }else
            {
                Companies.Add(company);
                return true;
            }
        }

        /// <summary>
        /// Remueve palabras clave de la oferta de una compania.
        /// Le delega la responsabilidad a company (La experta).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="keyWordIndex">La palabra clave.</param>
        public void RemoveKeyWords(Company company, IOffer offer, int keyWordIndex)
        {
            company.RemoveKeyWords(offer, keyWordIndex);
        }

        /// <summary>
        /// Agrega las palabras clave de una oferta.
        /// Le delaga la responsabilidad a Company (La epxerta).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="keyWord">La palabra clave.</param>
        public void AddKeyWords(Company company, IOffer offer, string keyWord)
        {
            company.AddKeyWords(offer,keyWord);
        }

        /// <summary>
        /// Remueve la oferta de una compania.
        /// Le delega la responsabilidad a company (La experta).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offerIndex">La oferta.</param>
        public void RemoveOffer(Company company, int offerIndex)
        {
            company.OffersPublished.RemoveAt(offerIndex);
        }

        /// <summary>
        /// Remueve las habilitaciones de una compania. 
        /// Le delega la responsabilidad a company (La experta).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="qualificationIndex">La habilitacion.</param>
        public void RemoveQualification(Company company, IOffer offer,int qualificationIndex)
        {
            company.RemoveQualification(offer, qualificationIndex);
        }

        /// <summary>
        /// Agrega habilitaciones a una oferta.
        /// Le delega la responsabilidad a company (La experta).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="qualification">La habilitacion.</param>
        public void AddQualification(Company company, IOffer offer, Qualifications qualification)
        {
            company.AddQualification(offer, qualification);
        }

        /// <summary>
        /// Publica una ofert constante de la compania que se le ingresa.
        /// ÑLe delaga la responsabilidad a company (Por patron creator).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="tipo">La clasificacion.</param>
        /// <param name="quantity">La cantidad.</param>
        /// <param name="cost">El precio.</param>
        /// <param name="ubication">La ubicacion.</param>
        /// <param name="qualifications">Las hablitaciones.</param>
        /// <param name="keyWords">La palabra clave.</param>
        public void PublicConstantOffer(Company company, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
        {
            company.PublicConstantOffer(tipo,quantity,cost,ubication,qualifications,keyWords);
        }

        /// <summary>
        /// Publica una oferta no constnte de la compania que se le ingresa.
        /// Le delega la responsabilidad a company (Por patron creator).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="tipo">La clasificacion.</param>
        /// <param name="quantity">La cantidad.</param>
        /// <param name="cost">El precio.</param>
        /// <param name="ubication">La ubicacion.</param>
        /// <param name="qualifications">Las hablitaciones.</param>
        /// <param name="keyWords">La palabra clave.</param>
        public void PublicNonConstantOffer(Company company, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
        {
            company.PublicNonConstantOffer(tipo,quantity,cost,ubication,qualifications,keyWords);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por tipo.
        /// Le delega la responsabilidada a OfferSearch (Por SRP).
        /// </summary>
        /// <param name="word">Tipo de oferta.</param>
        /// <returns>Un Lista con todas las ofertas que sean de ese tipo.</returns>
        public List<IOffer> SearchOfferByType(string word)
        {
            return OfferSearch.Instance.SearchByType(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por ubicacion.
        /// Le delega la responsabilidada a OfferSearch (Por SRP).
        /// </summary>
        /// <param name="word">Ubicacion de la oferta.</param>
        /// <returns>Un Lista con todas las ofertas en la ubicacion dada.</returns>
        public List<IOffer> SearchOfferByUbication(string word)
        {
            return OfferSearch.Instance.SearchByUbication(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por palabra clave.
        /// Le delega la responsabilidada a OfferSearch (Por SRP).
        /// </summary>
        /// <param name="keyWord">La palabra clave que debeena tener las ofertas.</param>
        /// <returns>Un Lista con todas las ofertas que tenga esa palabara clave.</returns>
        public List<IOffer> SearchOfferByKeywords(string keyWord)
        {
            return OfferSearch.Instance.SearchByKeywords(keyWord);
        }

        /// <summary>
        /// Metodo para aceptar una oferta.
        /// Le delega la responsabilidad de determinar si la oferta puede o no ser aceptada por el emprendedor, a Offer, la que tiene todos los datos para saber si es posible la accion (Por expert).
        /// Le delega la responsabilidad de agregar una oferta, a Emprendedor, la que que tiene la lista de ofertas que acepto (Por expert).
        /// AppLofic tiene la responsabiliad de enviar el mensaje de confirmacuon, porque es la que conoce todos los datos necesarios para determinar un resultado (Por expert).        /// </summary>
        /// <param name="emprendedor">Emprendedor.</param>
        /// <param name="offer">Oferta a aceptar.</param>
        /// <returns>Mensaje de confirmacion si la oferta fue o no aceptada.</returns>
        public bool AccepOffer(Emprendedor emprendedor, IOffer offer)
        {
            if (offer.PutBuyer(emprendedor))
            {
                emprendedor.AddPurchasedOffer(offer);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo que permite obtener la distancia entre un emprendedor y un producto.
        /// Utiliza la LocationApi, que esta en la clase singlestion LocationApiCointener.
        /// </summary>
        /// <param name="emprendedor">Emprendedor.</param>
        /// <param name="offer">La Oferta.</param>
        /// <returns>El string que marca la distancia.</returns>
        public string ObteinOfferDistance(Emprendedor emprendedor, IOffer offer)
        {
            string emprendedorUbication = emprendedor.Ubication;
            string offerUbication = offer.Product.Ubication;
            Location locationEmprendedor = client.GetLocation(emprendedorUbication);
            Location locationOffer = client.GetLocation(offerUbication);
            Distance distance = client.GetDistance(locationEmprendedor, locationOffer);
            double kilometers = distance.TravelDistance;
            client.DownloadRoute(locationEmprendedor.Latitude, locationEmprendedor.Longitude,
            locationOffer.Latitude, locationOffer.Longitude, @"route.png");
            return Convert.ToString(kilometers) + "Kilometros.";
        }

        /// <summary>
        /// Metodo que obtiene el mapa de la ubicacion de una oferta.
        /// Utiliza la LocationApi, que esta en la clase singlestion LocationApiCointener.
        /// </summary>
        /// <param name="offer">Oferta que se quiere buscar.</param>
        public void ObteinOfferMap(IOffer offer)
        {
            string offerUbication = offer.Product.Ubication;
            Location locationOffer = client.GetLocation(offerUbication);
            client.DownloadMap(locationOffer.Latitude, locationOffer.Longitude, @"map.png");
        }

        /// <summary>
        /// Metodo que devuelve un string con la lista de materiales constantes.
        /// Por expert tiene esta responsabilidad.
        /// </summary>
        /// <returns>Un diccionario con los materiales recurentes, y su respectiva conatdad de oferta.</returns>
        public Dictionary<string, int> GetConstantMaterials()
        {
            Dictionary<string, int> clasificationDictionary = new Dictionary<string, int>();
            ArrayList constantMaterials = new ArrayList();
            foreach(Classification item in Classifications)
            {
                clasificationDictionary.Add(item.Category,0);
            }
            foreach (Company company in Companies)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if (offer.GetType().Equals(typeof(ConstantOffer)))
                    {
                        constantMaterials.Add(offer.Product);
                        clasificationDictionary[offer.Product.Classification.Category] += 1;
                    }
                }
            }
            return clasificationDictionary;
        }

        /// <summary>
        /// Obtiene una lista de las ofertas que fueron acepatadas de la compañia que se le ingresa.
        /// Es una operacion polimorfica.
        /// Le delega la responsabilidad a Company (La experta).
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <returns>La lista con las ofertas que fueron aceptadas.</returns>
        public List<IOffer> GetOffersAccepted(Company company)
        {
            return company.GetOffersAccepted();
        }

        /// <summary>
        /// Obtiene una lista de las ofertas que fueron acepatadas por el emprendedor que se le ingresa.
        /// Le delega la responsabilidad a emprendedor, la experta.
        /// Es una operacion polimorfica.
        /// </summary>
        /// <param name="emprendedor">Emprendedor.</param>
        /// <returns>La luita con las ofertas aceptadas.</returns>
        public List<IOffer> GetOffersAccepted(Emprendedor emprendedor)
        {
            return emprendedor.PurchasedOffers;
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en un periodo de tiempo establecido por el usuario.
        /// Le delega la responsabilidad a company (La experta).
        /// Es una operacion polimorfica.
        /// </summary>
        /// <param name="company">Compania.</param>
        /// <param name="periodTime">Periodo de tiempo establecido por el usuario.</param>
        /// <returns></returns>
        public List<IOffer> GetOffersAccepted(Company company, int periodTime)
        {
            return company.GetOffersAccepted(periodTime);
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en un periodo de tiempo establecido por el usuario.
        /// Le delega la responasabilidad a emprendedor (La experta).
        /// Es una operaciion polimorfica.
        /// </summary>
        /// <param name="emprendedor">Emprendedor.</param>
        /// <param name="periodTime">Periodo de tiempo establecido por el usuario.</param>
        /// <returns>La lista con las ofertas.</returns>
        public List<IOffer> GetOffersAccepted(Emprendedor emprendedor, int periodTime)
        {
            return emprendedor.GetOffersAccepted(periodTime);
        }

        /// <summary>
        /// Obtiene la compania mediente el id de usuario ingreado.
        /// Como AppLogic tiene la lista de copanias, tiene esta responsabiliad (Es la experta).
        /// </summary>
        /// <param name="user_Id">El id de usuario.</param>
        /// <returns>La compnai que tiene ese id.</returns>
        public Company GetCompany(string user_Id)
        {
            foreach(Company item in this.Companies)
            {
                if (item.User_Id == user_Id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtiene el emprendedor mendiante el ingreso de un id de usuario.
        /// Como AppLogic tiene la lista de emprendedores, tiene esta respnsabilidad (Es la experta).
        /// </summary>
        /// <param name="user_Id">El id de usuario.</param>
        /// <returns>El emprendedor que tiene ese id.</returns>
        public Emprendedor GetEmprendedor(string user_Id)
        {
            foreach (Emprendedor item in this.Entrepreneurs)
            {
                if (item.User_Id == user_Id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
