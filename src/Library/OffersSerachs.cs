using System.Text;
using System.Collections.Generic;
using System;

namespace Proyect
{
    /// <summary>
    /// Clase que se encarga de buscar las ofertas por los distintos metodos establecidos (Por SRP).
    /// Es una clase singleton (solo hay una instancia de la misma).
    /// </summary>
    public sealed class OfferSearch
    {
        private readonly static OfferSearch _instance = new OfferSearch();

        private OfferSearch()
        {
        }

        /// <summary>
        /// Obtiene la instancia de OfferSearch.
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
        /// <returns>una lista con las offertas</returns>
        public List<IOffer> SearchByKeywords(string word)
        {
            string keyWord = word.Replace(".","");
            keyWord = keyWord.Replace(",","");
            keyWord = keyWord.Replace(" ","");
            List<IOffer> offersList = new List<IOffer>();
            foreach (Company company in AppLogic.Instance.Companies.Values)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if (offer.KeyWords.Contains(keyWord))
                    {
                        offersList.Add(offer);
                    }
                }
            }
            return offersList;
        }

        /// <summary>
        /// Busca ofertas por ubicacion.
        /// </summary>
        /// <param name="ubication">Ubicacion de oferta.</param>
        /// <returns>una lista con las offertas</returns>
        public List<IOffer> SearchByUbication(string ubication)
        {
            List<IOffer> offersList = new List<IOffer>();
            foreach (Company company in AppLogic.Instance.Companies.Values)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if(offer.Product.Ubication == ubication)
                    {
                        offersList.Add(offer);
                    }
                }
            }
            return offersList;
        }

        /// <summary>
        /// Busca ofertas por el tipo.
        /// </summary>
        /// <param name="type">Tipo de oferta.</param>
        /// <returns>una lista con las offertas</returns>
        public List<IOffer> SearchByType(string type)
        {
            List<IOffer> offersList = new List<IOffer>();
            foreach (Company company in AppLogic.Instance.Companies.Values)
            {
                foreach (IOffer offer in company.OffersPublished)
                {
                    if(offer.Product.Classification.Category == type)
                    {
                        offersList.Add(offer);
                    }
                }
            }
            return offersList;
        }
    }
}