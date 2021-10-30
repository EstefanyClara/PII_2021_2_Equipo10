using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

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

        private ArrayList validRubros = new ArrayList(){new Rubro("Alimentos"),new Rubro("Tecnologia"),new Rubro("Medicina")};

        private ArrayList validQualifications = new ArrayList(){new Qualifications("Vehiculo propio"),new Qualifications("Espacio para grandes volumenes de producto"),new Qualifications("Lugar habilitado para conservar desechos toxicos")};

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
        public ArrayList Rubros
        {
            get{return validRubros;}
        }

        /// <summary>
        /// Obtiene la lista de habilitciones registradas
        /// </summary>
        /// <value></value>
        public ArrayList Qualifications
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
            foreach (string item in Rubros)
            {
                itemposition++;
                message.Append($"{itemposition}-"+item+"\n"); 
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
            foreach (string item in Rubros)
            {
                itemposition++;
                message.Append($"{itemposition}-"+item+"\n"); 
            }
            return Convert.ToString(message);
        }

    }
}