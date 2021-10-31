using System; 
using System.Collections;
using System.Collections.Generic;

namespace Proyect
{
        /// <summary>
        /// Esta clase representa la compania, 
        /// </summary>
        public class Company: User
        {
                /// <summary>
                /// Lista de ofertas de la compania
                /// </summary>
                private ArrayList offers; 

                /// <summary>
                /// Constructor de company
                /// </summary>
                /// <param name="name"></param>
                /// <param name="ubication"></param>
                /// <param name="rubro"></param>
                public Company(string name, string ubication, Rubro rubro):base(name,ubication,rubro)
                {

                }

                /// <summary>
                /// Metodo que retorna una lista con las ofertas publicadas por la empresa
                /// </summary>
                /// <value></value>
                public ArrayList OffersPublished
                {
                        get{ return this.offers; }     
                }

                /// <summary>
                /// Publica una oferta,es decir,la crea y la guarda en su lista
                /// </summary>
                public  void PublicOffer(bool ifConstant, Classification tipo, int quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
                {
                        OffersPublished.Add(new Offer(ifConstant, tipo, quantity, cost, ubication, qualifications, keyWords));
                }
        }
}
