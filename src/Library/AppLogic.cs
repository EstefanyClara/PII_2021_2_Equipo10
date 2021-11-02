using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using LocationApi;
using System.Threading.Tasks;

namespace Proyect
{
    /// <summary>
    /// Clase singleton para guardar los datos de la Aplicacion.
    /// </summary>
    public sealed class AppLogic
    {
        private LocationApiClient client = new LocationApiClient();
        private readonly static AppLogic _instance = new AppLogic();
        private List<Company> companies;
        private List<Emprendedor> entrepreneurs;
        private List<Rubro> validRubros = new List<Rubro>(){new Rubro("Alimentos"),new Rubro("Tecnologia"),new Rubro("Medicina")};

        private List<Qualifications> validQualifications = new List<Qualifications>(){new Qualifications("Vehiculo propio"),new Qualifications("Espacio para grandes volumenes de producto"),new Qualifications("Lugar habilitado para conservar desechos toxicos")};

        private List<Classification> validClasification = new List<Classification>(){new Classification("Organicos"),new Classification("Plasticos"),new Classification("Alimentos"),new Classification("Toxicos")};

        /// <summary>
        /// Obtiene las companias que estan registradas.
        /// </summary>
        /// <value>Companias.</value>
        public List<Company> Companies
        {
            get{return companies;}
        }

        /// <summary>
        /// Obtiene los emprendedores que estan registrados.
        /// </summary>
        /// <value>Emprendedores.</value>
        public List<Emprendedor> Entrepreneurs
        {
            get{ return entrepreneurs;}
        }

        /// <summary>
        /// Obtiene los rubros habilitados.
        /// </summary>
        /// <value>Rubros.</value>
        public List<Rubro> Rubros
        {
            get{return validRubros;}
        }

        /// <summary>
        /// Obtiene la lista de habilitaciones registradas.
        /// </summary>
        /// <value>Habilitaciones.</value>
        public List<Qualifications> Qualifications
        {
            get{return validQualifications;}
        }

        /// <summary>
        /// Obtiene la lista de clasificaciones/categorias registradas para los productos.
        /// </summary>
        /// <value>Clasificaciones.</value>
        public List<Classification> Classifications
        {
            get{ return validClasification;}
        }
        private AppLogic()
        {
            companies = new List<Company>();
            entrepreneurs = new List<Emprendedor>();
        }

        /// <summary>
        /// Obtiene la instancia de AppLogic.
        /// </summary>
        /// <value>Instancias.</value>
        public static AppLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        
        /// <summary>
        /// Metodo que registra un emprendedor.
        /// </summary>
        /// <param name="name">El nombre del emprendedor.</param>
        /// <param name="ubication">La ubicacion del emprendedor.</param>
        /// <param name="rubro">El rubro del emprendedor.</param>
        /// <param name="habilitaciones">Las habilitaciones que tiene el emprendedor.</param>
        /// <param name="especializaciones">Las especializaciones que tiene el emprendedor.</param>//  
        public void RegisterEntrepreneurs(string name, string ubication, Rubro rubro, List<Qualifications> habilitaciones,List<Qualifications> especializaciones)
        {
            try
            {
                entrepreneurs.Add(new Emprendedor(name, ubication, rubro, habilitaciones, especializaciones));
            }
            catch (EmptyUserBuilderException e)
            {
                Console.WriteLine("Error al registrase");
                throw e;
            }
        }

        /// <summary>
        /// Metodo que retorna un mensaje con los rubros habilitados.
        /// </summary>
        /// <returns>Los rubros habilitados.</returns>
        public string ValidRubrosMessage()
        {
            StringBuilder message = new StringBuilder("Rubros habilitados:\n\n");
            int itemposition = 0;
            foreach (Rubro item in Rubros)
            {
                itemposition++;
                message.Append($"{itemposition}-"+item.RubroName+"\n"); 
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Metodo que retorna un mensaje con las Habilitaciones permitidas.
        /// </summary>
        /// <returns>Habilitaciones permitidas.</returns>
        public string validQualificationsMessage()
        {
            StringBuilder message = new StringBuilder("Habilitaciones permitidas:\n\n");
            int itemposition = 0;
            foreach (Qualifications item in Qualifications)
            {
                itemposition++;
                message.Append($"{itemposition}-"+item.QualificationName+"\n"); 
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Remueve palabras clave de la oferta de una compania.
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="keyWord">La palabra clave.</param>
        public void RemoveKeyWords(Company company, Offer offer, string keyWord)
        {
            company.RemoveKeyWords(offer, keyWord);
        }

        /// <summary>
        /// Agrega las palabras clave de una oferta.
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="keyWord">La palabra clave.</param>
        public void AddKeyWords(Company company, Offer offer, string keyWord)
        {
            company.AddKeyWords(offer, keyWord);
        }

        /// <summary>
        /// Remueve la oferta de una compania.
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        public void RemoveOffer(Company company, Offer offer)
        {
            company.RemoveOffer(offer);
        }

        /// <summary>
        /// Remueve las habilitaciones de una compania. 
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="qualification">La habilitacion.</param>
        public void RemoveQualification(Company company, Offer offer, Qualifications qualification)
        {
            company.RemoveQualification(offer, qualification);
        }

        /// <summary>
        /// Agrega habilitaciones a una oferta.
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="offer">La oferta.</param>
        /// <param name="qualification">La habilitacion.</param>
        public void AddQualification(Company company, Offer offer, Qualifications qualification)
        {
            company.AddQualification(offer, qualification);
        }

        /// <summary>
        /// Publica una oferta de la compania que se le ingresa.
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <param name="ifConstant">Si es recurrente.</param>
        /// <param name="tipo">La clasificacion.</param>
        /// <param name="quantity">La cantidad.</param>
        /// <param name="cost">El precio.</param>
        /// <param name="ubication">La ubicacion.</param>
        /// <param name="qualifications">Las hablitaciones.</param>
        /// <param name="keyWords">La palabra clave.</param>
        public void PublicOffer(Company company,bool ifConstant, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
        {
            company.PublicOffer(ifConstant,tipo,quantity,cost,ubication,qualifications,keyWords);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por palabras clave.
        /// </summary>
        /// <param name="word">Palabra clave.</param>
        /// <returns>Un ArrayList con las ofertas que tengan la palabra clave.</returns>
        public ArrayList SearchOfferByKeyWords(string word)
        {
            return OfferSearch.SearchByKeywords(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por tipo.
        /// </summary>
        /// <param name="word">Tipo de oferta.</param>
        /// <returns>Un ArrayList con todas las ofertas que sean de ese tipo.</returns>
        public ArrayList SearchOfferByType(string word) // duda.
        {
            return OfferSearch.SearchByType(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por ubicacion.
        /// </summary>
        /// <param name="word">Ubicacion de la oferta.</param>
        /// <returns>Un ArrayList con todas las ofertas en la ubicacion dada.</returns>
        public ArrayList SearchOfferByUbication(string word)
        {
            return OfferSearch.SearchByUbication(word);
        }

        /// <summary>
        /// Metodo para aceptar una oferta.
        /// </summary>
        /// <param name="emprendedor">Emprendedor.</param>
        /// <param name="offer">Oferta a aceptar.</param>
        public string AccepOffer(Emprendedor emprendedor, Offer offer)
        {
            foreach(Qualifications item in offer.Qualifications)
            {
                if(!emprendedor.Qualifications.Contains(item))
                {
                    return "Usted no dispone de las habilitaciones requeridas por la oferta";
                }
            }
            PurchaseData pd = new PurchaseData(emprendedor);
            offer.PurchaseData = pd;
            // offer.Buyer = emprendedor;
            // offer.TimeAccepted = DateTime.Now;
            emprendedor.AddPurchasedOffer(offer);
            return "Usted a aceptado la oferta con exito";
        }

        /// <summary>
        /// Metodo que permite obtener la distancia entre un emprendedor y un producto.
        /// </summary>
        public async Task<double> ObteinOfferDistance(Emprendedor emprendedor, Offer offer)
        {
            string emprendedorUbication = emprendedor.Ubication;
            string offerUbication = offer.Product.Ubication;
            Location locationEmprendedor = await client.GetLocation(emprendedorUbication);
            Location locationOffer = await client.GetLocation(offerUbication);
            Distance distance = await client.GetDistance(locationEmprendedor, locationOffer);
            double kilometers = distance.TravelDistance;
            await client.DownloadRoute(locationEmprendedor.Latitude, locationEmprendedor.Longitude,
            locationOffer.Latitude, locationOffer.Longitude, @"route.png");
            return kilometers;
        }

        /// <summary>
        /// Metodo que obtiene el mapa de la ubicacion de un emprendedor.
        /// </summary>
        /// <param name="offer">Oferta que se quiere buscar.</param>
        /// <returns>Un mapa de la ubicacion del emprendedor.</returns>
        public async Task ObteinOfferMap(Offer offer)
        {
            string offerUbication = offer.Product.Ubication;
            Location locationOffer = await client.GetLocation(offerUbication);
            await client.DownloadMap (locationOffer.Latitude, locationOffer.Longitude, @"map.png");
        }

        /// <summary>
        /// Metodo que devuelve un string con la lista de materiales constantes.
        /// </summary>
        /// <returns>Un string con aquellos materiales que son recuerrentes.</returns>
        public (ArrayList, string) GetConstantMaterials()
        {
            Dictionary<Classification, int> clasificationDictionary = new Dictionary<Classification, int>();
            ArrayList constantMaterials = new ArrayList();
            foreach(Classification item in Classifications)
            {
                clasificationDictionary.Add(item,0);
            }
            foreach (Company company in Companies)
            {
                foreach (Offer offer in company.OffersPublished)
                {
                    if (offer.Constant)
                    {
                        constantMaterials.Add(offer.Product);
                        clasificationDictionary[offer.Product.Classification] += 1;
                    }
                }
            }
            StringBuilder message = new StringBuilder();
            message.Append("Los tipos de materiales mas constantes en nuestras ofertas son:\n\n");
            foreach(Classification item in Classifications)
            {
                message.Append($"{item.Category} con {clasificationDictionary[item]} ofertas\n");
            }
            return (constantMaterials,Convert.ToString(message));
        }

        /// <summary>
        /// Obtiene un string con la indicando si sus ofertas fueron o no fueron aceptadas, en caso de que si, indica ademas la fecha de cuando fueron aceptadas.
        /// </summary>
        /// <param name="company">La compania.</param>
        /// <returns>Un string con las ofertas que fueron aceptadas.</returns>
        public string GetOffersAccepted(Company company)
        {
            return company.GetOffersAccepted();
        }

        /// <summary>
        /// Obtiene las ofertas aceptadas por el emprendedor, junto con la fecha de cuando las acepto.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        public string GetOffersAccepted(Emprendedor emprendedor)
        {
            return emprendedor.GetOffersAccepted();
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en un periodo de tiempo establecido por el usuario.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="periodTime"></param>
        /// <returns></returns>
        public int GetPeriodTimeOffersAccepted(Company company, int periodTime)
        {
            return company.GetPeriodTimeOffersAccepted(periodTime);
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en un periodo de tiempo establecido por el usuario.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <param name="periodTime"></param>
        /// <returns></returns>
        public int GetPeriodTimeOffersAccepted(Emprendedor emprendedor, int periodTime)
        {
            return emprendedor.GetPeriodTimeOffersAccepted(periodTime);
        }
    }
}
