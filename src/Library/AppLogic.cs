using System;
using System.Collections;

namespace Proyect
{
    /// <summary>
    /// Clase singleton para guardar los datos de la Aplicacion
    /// </summary>
    public sealed class AppLogic
    {
        private readonly static AppLogic _instance = new AppLogic();
        private ArrayList companies;
        private ArrayList entrepreneurs;

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
        /// Busca las ofertas con la palabra clave que se le pasa
        /// </summary>
        /// <param name="keyword"></param>
        public void SearchByKeywords(string keyword)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones
            ArrayList offers = new ArrayList();
            foreach (Company company in companies)
            {
                foreach (Offer offer in company.offers)
                {
                    foreach (string word in offer.KeyWords)
                    {
                        if(word == keyword)
                        {
                            offers.Add(offer);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Busca ofertas por ubicacion
        /// </summary>
        /// <param name="ubication"></param>
        public void SearchByUbication(string ubication)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones
            ArrayList offers = new ArrayList();
            foreach (Company company in companies)
            {
                foreach (Offer offer in company.offers)
                {
                    if(offer.Product.Ubication == ubication)
                    {
                        offers.Add(offer);
                    }
                }
            }
        }

        /// <summary>
        /// Busca ofertas por el tipo
        /// </summary>
        /// <param name="type"></param>
        public void SearchByType(string type)
        {
            // Deberia funcionar, pero hay que adaptar para cumplir patrones
            ArrayList offers = new ArrayList();
            foreach (Company company in companies)
            {
                foreach (Offer offer in company.offers)
                {
                    if(offer.Product.Classification.Category == type)
                    {
                        offers.Add(offer);
                    }
                }
            }
        }

    }
}