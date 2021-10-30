using System; 
using System.Collections;

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
                public ArrayList offers; 

                /// <summary>
                /// Constructor de company
                /// </summary>
                /// <param name="name"></param>
                /// <param name="ubication"></param>
                /// <param name="rubro"></param>
                public Company(string name, string ubication, string rubro):base(name,ubication,rubro)
                {

                }

                /// <summary>
                /// Publica una oferta,es decir,la crea y la guarda en su lista
                /// </summary>
                public  void PublicOffer()
                {
        
                }
        }
}
