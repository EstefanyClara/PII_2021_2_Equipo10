using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using LocationApi;
using System.Threading.Tasks;

namespace Proyect
{
    /// <summary>
    /// Clase singleton para guardar los datos de la Aplicacion
    /// </summary>
    public sealed class AppLogic
    {
        private LocationApiClient client = APILocationContainer.Instance.APIdeLocalizacion;
        private readonly static AppLogic _instance = new AppLogic();
        private List<Company> companies;
        private List<Emprendedor> entrepreneurs;
        private List<Rubro> validRubros = new List<Rubro>(){new Rubro("Alimentos"),new Rubro("Tecnologia"),new Rubro("Medicina")};

        private List<Qualifications> validQualifications = new List<Qualifications>(){new Qualifications("Vehiculo propio"),new Qualifications("Espacio para grandes volumenes de producto"),new Qualifications("Lugar habilitado para conservar desechos toxicos")};

        private List<Classification> validClasification = new List<Classification>(){new Classification("Organicos"),new Classification("Plasticos"),new Classification("Alimentos"),new Classification("Toxicos")};

        /// <summary>
        /// Obtiene las companias que estan registradas
        /// </summary>
        /// <value>companies</value>
        public List<Company> Companies
        {
            get{return companies;}
        }

        /// <summary>
        /// Obtiene los emprendedores que estan registrados
        /// </summary>
        /// <value>entrepreneurs</value>
        public List<Emprendedor> Entrepreneurs
        {
            get{ return entrepreneurs;}
        }

        /// <summary>
        /// Obtiene los rubros habilitados
        /// </summary>
        /// <value></value>
        public List<Rubro> Rubros
        {
            get{return validRubros;}
        }

        /// <summary>
        /// Obtiene la lista de habilitciones registradas
        /// </summary>
        /// <value></value>
        public List<Qualifications> Qualifications
        {
            get{return validQualifications;}
        }

        /// <summary>
        /// Obtiene la lista de clasificaciones/categorias registradas para los productos
        /// </summary>
        /// <value></value>
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
        /// Obtiene la instancia de AppLogic
        /// </summary>
        /// <value></value>
        public static AppLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        
        /// <summary>
        /// Metodo que registra a un emprendedor
        /// </summary>
        public string RegisterEntrepreneurs(string name, string ubication, Rubro rubro, List<Qualifications> habilitaciones,ArrayList especializaciones)
        {
            try
            {
                entrepreneurs.Add(new Emprendedor(name,ubication,rubro,habilitaciones, especializaciones));
            }
            catch (EmptyUserBuilderException e)
            {
                Console.WriteLine("Error al registrase");
                throw e;
            }

            return "Usted se a registrado con exito";
        }

        /// <summary>
        /// Metodo que retorna un mensaje con los rubros habilitaddos
        /// </summary>
        /// <returns></returns>
        public string ValidRubrosMessage()
        {
            StringBuilder message = new StringBuilder("Rubros habiliatdos:\n\n");
            int itemposition = 0;
            foreach (Rubro item in Rubros)
            {
                itemposition++;
                message.Append($"{itemposition}-"+item.RubroName+"\n"); 
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Metdo que retorna un mensaje con las Habilitaciones permitidas
        /// </summary>
        /// <returns></returns>
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
        /// Remueve palabras clave de la oferta de una compania
        /// </summary>
        /// <param name="company"></param>
        /// <param name="offer"></param>
        /// <param name="keyWord"></param>
        public string RemoveKeyWords(Company company,IOffer offer, string keyWord)
        {
            company.RemoveKeyWords(offer,keyWord);
            return $"{keyWord} removida con exito";
        }

        /// <summary>
        /// Remueve las palabras clave de una oferta
        /// </summary>
        /// <param name="company"></param>
        /// <param name="offer"></param>
        /// <param name="keyWord"></param>
        public string AddKeyWords(Company company, IOffer offer, string keyWord)
        {
            return company.AddKeyWords(offer,keyWord);
        }

        /// <summary>
        /// Remueve la oferta de una compania
        /// </summary>
        /// <param name="company"></param>
        /// <param name="offer"></param>
        public string RemoveOffer(Company company, IOffer offer)
        {
            company.RemoveOffer(offer);
            return "Oferta removida con exito";
        }

        /// <summary>
        /// Remueve las habilitaciones de una oferta
        /// </summary>
        /// <param name="company"></param>
        /// <param name="offer"></param>
        /// <param name="qualification"></param>
        public string RemoveQualification(Company company, IOffer offer, Qualifications qualification)
        {
            company.RemoveQualification(offer, qualification);
            return "Habilitacion removida";
        }

        /// <summary>
        /// Agrega habilitaciones a una oferta
        /// </summary>
        /// <param name="company"></param>
        /// <param name="offer"></param>
        /// <param name="qualification"></param>
        public string AddQualification(Company company, IOffer offer, Qualifications qualification)
        {
            company.AddQualification(offer,qualification);
            return "Se a agrgado la habilitacion";
        }

        /// <summary>
        /// Publica una oferta constante de la compania que se le ingresa
        /// </summary>
        /// <param name="company"></param>
        /// <param name="tipo"></param>
        /// <param name="quantity"></param>
        /// <param name="cost"></param>
        /// <param name="ubication"></param>
        /// <param name="qualifications"></param>
        /// <param name="keyWords"></param>
        public string PublicConstantOffer(Company company, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
        {
            company.PublicConstantOffer(tipo,quantity,cost,ubication,qualifications,keyWords);
            return "Oferta publicada con exito";
        }

        /// <summary>
        /// Publica una oferta no constante de la compania que se le ingresa
        /// </summary>
        /// <param name="company"></param>
        /// <param name="tipo"></param>
        /// <param name="quantity"></param>
        /// <param name="cost"></param>
        /// <param name="ubication"></param>
        /// <param name="qualifications"></param>
        /// <param name="keyWords"></param>
        public string PublicNonConstantOffer(Company company, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
        {
            company.PublicNonConstantOffer(tipo,quantity,cost,ubication,qualifications,keyWords);
            return "Oferta publicada con exito";
        }
        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por palabras clave
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<string> SearchOfferByKeyWords(string word)
        {
            return OfferSearch.Instance.SearchByKeywords(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por tipo
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<string> SearchOfferByType(string word)
        {
            return OfferSearch.Instance.SearchByType(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por ubicacion
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<string> SearchOfferByUbication(string word)
        {
            return OfferSearch.Instance.SearchByUbication(word);
        }

        /// <summary>
        /// Metodo para aceptar una oferta
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <param name="offer"></param>
        public string AccepOffer(Emprendedor emprendedor, IOffer offer)
        {
            foreach(Qualifications item in offer.Qualifications)
            {
                if(!emprendedor.Qualifications.Contains(item))
                {
                    return "Usted no dispone de las habilitaciones requeridas por la oferta";
                }
            }
            offer.PutBuyer(emprendedor, DateTime.Now);
            emprendedor.AddPurchasedOffer(offer);
            return "Usted a aceptado la oferta con exito";
        }

        /// <summary>
        /// Metodo que permite obtener la distancia entre un emprendedor y un producto
        /// </summary>
        public async Task<string> ObteinOfferDistance(Emprendedor emprendedor, IOffer offer)
        {
            string emprendedorUbication = emprendedor.Ubication;
            string offerUbication = offer.Product.Ubication;
            Location locationEmprendedor = await client.GetLocation(emprendedorUbication);
            Location locationOffer = await client.GetLocation(offerUbication);
            Distance distance = await client.GetDistance(locationEmprendedor, locationOffer);
            double kilometers = distance.TravelDistance;
            await client.DownloadRoute(locationEmprendedor.Latitude, locationEmprendedor.Longitude,
            locationOffer.Latitude, locationOffer.Longitude, @"route.png");
            return Convert.ToString(kilometers);
        }

        /// <summary>
        /// Metodo que obtiene el mapa de la ubicacion de un emprendedor
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public async Task ObteinOfferMap(IOffer offer)
        {
            string offerUbication = offer.Product.Ubication;
            Location locationOffer = await client.GetLocation(offerUbication);
            await client.DownloadMap(locationOffer.Latitude, locationOffer.Longitude, @"map.png");
        }

        /// <summary>
        /// Metodo que devuelbe un string con la lista de materiales constantes
        /// </summary>
        /// <returns></returns>
        public string GetConstantMaterials()
        {
            Dictionary<Classification, int> clasificationDictionary = new Dictionary<Classification, int>();
            ArrayList constantMaterials = new ArrayList();
            foreach(Classification item in Classifications)
            {
                clasificationDictionary.Add(item,0);
            }
            foreach (Company company in Companies)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if (offer.GetType().Equals(typeof(ConstantOffer)))
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
                if(clasificationDictionary[item] > 0)
                {
                message.Append($"Materiales {item.Category} con {clasificationDictionary[item]} oferta/s\n");
                }
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Obtiene un string con la indicando si sus ofertas fueron o no fueron aceptadas, en caso de que si, indica ademas la fecha de cuando fueron aceptadas
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string GetOffersAccepted(Company company)
        {
            return company.GetOffersAccepted();
        }

        /// <summary>
        /// Obtiene las ofertas aceptadas por el emprendedor, junto con la fecha de cuando las acepto
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        public string GetOffersAccepted(Emprendedor emprendedor)
        {
            return emprendedor.GetOffersAccepted();
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que furon aceptadas en un periodo de tiempo establecido por el usuario
        /// </summary>
        /// <param name="company"></param>
        /// <param name="periodTime"></param>
        /// <returns></returns>
        public string GetPeriodTimeOffersAccepted(Company company, int periodTime)
        {
            return company.GetPeriodTimeOffersAccepted(periodTime);
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que furon aceptadas en un periodo de tiempo establecido por el usuario
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <param name="periodTime"></param>
        /// <returns></returns>
        public string GetPeriodTimeOffersAccepted(Emprendedor emprendedor, int periodTime)
        {
            return emprendedor.GetPeriodTimeOffersAccepted(periodTime);
        }
    }
}
