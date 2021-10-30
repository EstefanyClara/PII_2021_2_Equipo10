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
        private LocationApiClient client = new LocationApiClient();
        private readonly static AppLogic _instance = new AppLogic();
        private ArrayList companies;
        private ArrayList entrepreneurs;

        private List<Rubro> validRubros = new List<Rubro>(){new Rubro("Alimentos"),new Rubro("Tecnologia"),new Rubro("Medicina")};

        private List<Qualifications> validQualifications = new List<Qualifications>(){new Qualifications("Vehiculo propio"),new Qualifications("Espacio para grandes volumenes de producto"),new Qualifications("Lugar habilitado para conservar desechos toxicos")};

        /// <summary>
        /// Obtiene las companias que estan registradas
        /// </summary>
        /// <value>companies</value>
        public ArrayList Companies
        {
            get{return new ArrayList(companies);}
        }

        /// <summary>
        /// Obtiene los emprendedores que estan registrados
        /// </summary>
        /// <value>entrepreneurs</value>
        public ArrayList Entrepreneurs
        {
            get{ return new ArrayList(entrepreneurs);}
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
        private AppLogic()
        {
            companies = new ArrayList();
            entrepreneurs = new ArrayList();
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
        public void RegisterEntrepreneurs(string name, string ubication, Rubro rubro, List<Qualifications> habilitaciones,List<Qualifications> especializaciones)
        {
            entrepreneurs.Add(new Emprendedor(name,ubication,rubro,habilitaciones, especializaciones));
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
        /// Metodo que se encarga de buscar las ofertas por palabras clave
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public ArrayList SearchOfferByKeyWords(string word)
        {
            return OfferSearch.SearchByKeywords(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por tipo
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public ArrayList SearchOfferByType(string word)
        {
            return OfferSearch.SearchByType(word);
        }

        /// <summary>
        /// Metodo que se encarga de buscar las ofertas por ubicacion
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public ArrayList SearchOfferByUbication(string word)
        {
            return OfferSearch.SearchByUbication(word);
        }

        /// <summary>
        /// Metodo que permite obtener la distancia entre un emprendedor y un producto
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
        /// Metodo que obtiene el mapa de la ubicacion de un emprendedor
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public async Task ObteinOfferMap(Offer offer)
        {
            string offerUbication = offer.Product.Ubication;
            Location locationOffer = await client.GetLocation(offerUbication);
            await client.DownloadMap(locationOffer.Latitude, locationOffer.Longitude, @"map.png");
        }

        

    }

}
