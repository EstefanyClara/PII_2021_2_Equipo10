using System.Text;
using System.Collections.Generic;
using System;

namespace Proyect
{
    /// <summary>
    /// Clase que se encarga de buscar las ofertas por los distintos metodos establecidos.
    /// </summary>
    public sealed class OfferSearch
    {
        private readonly static OfferSearch _instance = new OfferSearch();

        private OfferSearch()
        {
        }

        /// <summary>
        /// Obtiene la instancia de OfferSearch
        /// </summary>
        /// <value></value>
        public static OfferSearch Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Busca las ofertas con la palabra clave que se le pasa.
        /// </summary>
        /// <param name="word">Palabras claves de oferta.</param>
        public List<string> SearchByKeywords(string word)
        {
            string keyWord = word.Replace(".","");
            keyWord = keyWord.Replace(",","");
            keyWord = keyWord.Replace(" ","");
            List<string> offersList = new List<string>();
            foreach (Company company in AppLogic.Instance.Companies)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if (offer.KeyWords.Contains(keyWord))
                    {
                        offersList.Add(GetOffersMessages(offer,company));
                    }
                }
            }
            return offersList;
        }

        /// <summary>
        /// Busca ofertas por ubicacion.
        /// </summary>
        /// <param name="ubication">Ubicacion de oferta.</param>
        public List<string> SearchByUbication(string ubication)
        {
            List<string> offersList = new List<string>();
            foreach (Company company in AppLogic.Instance.Companies)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if(offer.Product.Ubication == ubication)
                    {
                        offersList.Add(GetOffersMessages(offer,company));
                    }
                }
            }
            return offersList;
        }

        /// <summary>
        /// Busca ofertas por el tipo.
        /// </summary>
        /// <param name="type">Tipo de oferta.</param>
        public List<string> SearchByType(string type)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones
            List<string> offersList = new List<string>();
            foreach (Company company in AppLogic.Instance.Companies)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if(offer.Product.Classification.Category == type)
                    {
                        offersList.Add(GetOffersMessages(offer,company));
                    }
                }
            }
            return offersList;
        }

        /// <summary>
        /// Obtiene la informacion de un oferta en fomra de mensaje.
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="company"></param>
        /// <returns>La infromacion de la oferta.</returns>
        public string GetOffersMessages(IOffer offer, Company company)
        {
            StringBuilder offerMessage = new StringBuilder();
            StringBuilder qualificationMessage = new StringBuilder();
            foreach (Qualifications item in offer.Qualifications)
            {
                qualificationMessage.Append($"|{item.QualificationName}|");
            }
            offerMessage.Append($"{offer.Product.Quantity} de {offer.Product.Classification.Category}\n\nCompania ofertora: {company.Name}\nPrecio: {offer.Product.Price}$\nUbicacion: {offer.Product.Ubication}\nHabilitaciones necesarias: {qualificationMessage}");
            return Convert.ToString(offerMessage);
        }
    }
}