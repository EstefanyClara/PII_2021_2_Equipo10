using System; 
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyect
{
        /// <summary>
        /// Esta clase representa la compania y hereda de user (cumple con el principio ISP, porque no depende de tipos que no usa, ya que utiliza todos los metodos y propiedades de user).
        /// </summary>
        public class Company: User
        {
                /// <summary>
                /// Lista de ofertas de la compania.
                /// </summary>
                private List<IOffer> offers = new List<IOffer>(); 

                /// <summary>
                /// Inicializa una nueva instancia de la clase <see cref="Company"/>.
                /// </summary>
                /// <param name="name">Nombre compania.</param>
                /// <param name="ubication">Ubicacion de la compania.</param>
                /// <param name="rubro">Rubro de la compania.</param>
                public Company(string name, string ubication, Rubro rubro):base(name, ubication, rubro)
                {

                }

                /// <summary>
                /// Metodo que retorna una lista con las ofertas publicadas por la empresa.
                /// </summary>
                /// <value>this.offers</value>
                public List<IOffer> OffersPublished
                {
                        get{ return this.offers; }     
                }

                /// <summary>
                /// Crea una instancia de una oferta constante y se la agrega a la lisat de ofertas de la compania.
                /// Se Asigno esta responsabilidad por expert (La clase company es la que conoce la lista de ls ofertas que publica).
                /// Tambien, se asigno esta reponsabilidad siguinedo el patron creator, company contiene objetos IOffer
                /// </summary>
                /// <param name="tipo"></param>
                /// <param name="quantity"></param>
                /// <param name="cost"></param>
                /// <param name="ubication"></param>
                /// <param name="qualifications"></param>
                /// <param name="keyWords"></param>
                public  void PublicConstantOffer(Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
                {
                        OffersPublished.Add(new ConstantOffer(tipo, quantity, cost, ubication, qualifications, keyWords));
                }

                /// <summary>
                /// Crea una instacion de una oferta no constante y se la agrega a la lista de ofertas de la compania.
                /// Se Asigno esta responsabilidad por expert (La clase company es la que conoce la lista de ls ofertas que publica).
                /// Tambien, se asigno esta reponsabilidad siguinedo el patron creator, company contiene objetos IOffer.
                /// </summary>
                /// <param name="tipo"></param>
                /// <param name="quantity"></param>
                /// <param name="cost"></param>
                /// <param name="ubication"></param>
                /// <param name="qualifications"></param>
                /// <param name="keyWords"></param>
                public  void PublicNonConstantOffer(Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
                {
                        this.OffersPublished.Add(new NonConstantOffer(tipo, quantity, cost, ubication, qualifications, keyWords));
                }

                /// <summary>
                /// Remueve ofertas (Por Expert).
                /// </summary>
                /// <param name="offer">Oferta.</param>
                public bool RemoveOffer(IOffer offer)
                {
                        if (this.OffersPublished.Contains(offer))
                        {
                                this.OffersPublished.Remove(offer);
                                return true;
                        }
                        return false;
                }

                /// <summary>
                /// Obtiene un string con todas las ofertas que fueron o no fueron aceptadas por emprendedores(por expert le asignamos esta responsabilidad).
                /// Es una operacion polimorfica.
                /// </summary>
                /// <returns>Las ofertas aceptadas.</returns>
                public List<IOffer> GetOffersAccepted()
                {
                        List<IOffer> ofertas = new List<IOffer>();
                        foreach (IOffer item in this.OffersPublished)
                        {
                                if (item != null)
                                {
                                        ofertas.Add(item);
                                }
                        }
                        return ofertas;
                }



                /// <summary>
                /// Obtiene la cantidad de ofertas que publico la compania, que fueron aceptadas en un periodo de tiempo (Por expert).
                /// Es una operwacion polimorfica.
                /// </summary>
                /// <param name="periodTime">Periodo de tiempo.</param>
                /// <returns>Las ofertas aceptadas en un periodo de tiempo.</returns>
                public List<IOffer> GetOffersAccepted(int periodTime)
                {
                        List<IOffer> ofertas = new List<IOffer>();
                        foreach(IOffer offer in this.OffersPublished)
                        {
                                if (offer.GetPeriodTimeOffersAcceptedData(periodTime))
                                {
                                        ofertas.Add(offer);
                                }
                        }
                        return ofertas;
                }

                /// <summary>
                /// Remueve palabras clave de una oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="keyWord">La palabra clave de la oferta.</param>
                public bool RemoveKeyWords(IOffer offer, string keyWord)
                {
                        if (offer.KeyWords.Contains(keyWord))
                        {
                                offer.KeyWords.Remove(keyWord);
                                return true;
                        }
                        return false;
                }

                /// <summary>
                /// Agrega palabras clave a una oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="keyWord">La palabra clave de la oferta.</param>
                public bool AddKeyWords(IOffer offer, string keyWord)
                {                
                        if (!offer.KeyWords.Contains(keyWord))
                        {
                                offer.KeyWords.Add(keyWord);
                                return true;
                        }
                        return false;
                }

                /// <summary>
                /// Agrega habilitaciones a la oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="qualification">La habilitaciones de la oferta.</param>
                public bool AddQualification(IOffer offer, Qualifications qualification)
                {
                        if (offer.Qualifications.Contains(qualification))
                        {
                                return false;
                        }
                        offer.Qualifications.Add(qualification);
                        return true;
                }

                /// <summary>
                /// Remueve la habilitacion de una oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="qualification">La habilitacion de la oferta.</param>
                public bool RemoveQualification(IOffer offer, Qualifications qualification)
                {
                        if (!offer.Qualifications.Contains(qualification))
                        {
                                return false;
                        }
                        offer.Qualifications.Remove(qualification);
                        return true;
                }
        }
}
