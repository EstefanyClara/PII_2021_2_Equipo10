using System.Collections;
namespace Proyect
{

    /// <summary>
    /// Clase que se encarga de buscar las ofertas por los distintos metodos establecidos.
    /// </summary>
    public static class OfferSearch
    {
        /// <summary>
        /// Busca las ofertas con la palabra clave que se le pasa.
        /// </summary>
        /// <param name="words">Palabras claves de oferta.</param>
        public static ArrayList SearchByKeywords(string words)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones.
            string[] keywords = words.Split(" ");
            ArrayList offers = new ArrayList();
            foreach (Company company in AppLogic.Instance.Companies)
            {
                foreach (Offer offer in company.OffersPublished)
                {

                    if (offer.KeyWords.Contains(keywords))
                    {
                        offers.Add(offer);
                    }
                }
            }
            return offers;
        }

        /// <summary>
        /// Busca ofertas por ubicacion.
        /// </summary>
        /// <param name="ubication">Ubicacion de oferta.</param>
        public static ArrayList SearchByUbication(string ubication)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones.
            ArrayList offers = new ArrayList();
            foreach (Company company in AppLogic.Instance.Companies)
            {
                foreach (Offer offer in company.OffersPublished)
                {
                    if(offer.Product.Ubication == ubication)
                    {
                        offers.Add(offer);
                    }
                }
            }
            return offers;
        }

        /// <summary>
        /// Busca ofertas por el tipo.
        /// </summary>
        /// <param name="type">Tipo de oferta.</param>
        public static ArrayList SearchByType(string type)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones.
            ArrayList offers = new ArrayList();
            foreach (Company company in AppLogic.Instance.Companies)
            {
                foreach (Offer offer in company.OffersPublished)
                {
                    if(offer.Product.Classification.Category == type)
                    {
                        offers.Add(offer);
                    }
                }
            }
            return offers;
        }
    }
}