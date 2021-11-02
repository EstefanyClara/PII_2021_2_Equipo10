using System; 
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyect
{
        /// <summary>
        /// Esta clase representa la compania y hereda de user (cumple con el principio ISP, porque no depende de tipos que no usa, ya que utiliza todos los metodos y propiedades de user)
        /// </summary>
        public class Company: User
        {
                /// <summary>
                /// Lista de ofertas de la compania
                /// </summary>
                private List<IOffer> offers = new List<IOffer>(); 

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
                public List<IOffer> OffersPublished
                {
                        get{ return this.offers; }     
                }

                /// <summary>
                /// Crea una instancia de una oferta constante y se la agrega a la lisat de ofertas de la compania
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
                /// Crea una instacion de una oferta no constante y se la agrega a la lista de ofertas de la compania
                /// </summary>
                /// <param name="tipo"></param>
                /// <param name="quantity"></param>
                /// <param name="cost"></param>
                /// <param name="ubication"></param>
                /// <param name="qualifications"></param>
                /// <param name="keyWords"></param>
                public  void PublicNonConstantOffer(Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
                {
                        OffersPublished.Add(new NonConstantOffer(tipo, quantity, cost, ubication, qualifications, keyWords));
                }

                /// <summary>
                /// Remueve ofertas
                /// </summary>
                /// <param name="offer"></param>
                public void RemoveOffer(IOffer offer)
                {
                        this.OffersPublished.Remove(offer);
                }

                /// <summary>
                /// Obtiene un string con todas las ofertas que fueron o no fueron aceptadas por emprendedores(por expert le asignamos esta responsabilidad)
                /// </summary>
                /// <returns></returns>
                public string GetOffersAccepted()
                {
                        StringBuilder message = new StringBuilder();
                        message.Append("La informacion de compra de sus ofertas es la siguiente\n\n");
                        foreach (IOffer item in this.OffersPublished)
                        {
                                message.Append(item.GetPurchesedData());
                        }
                        return Convert.ToString(message);
                }



                /// <summary>
                /// Obteien la cantidad de ofertas que publico la compania, que fueron aceptadas en cierto tiempo estipulado. (por expert)
                /// </summary>
                /// <param name="periodTime"></param>
                /// <returns>retorna un mensaje con la informacion de compra de las ofertas que entran en el rango indicado</returns>
                public string GetPeriodTimeOffersAccepted(int periodTime)
                {
                        int offersAccepted = 0;
                        StringBuilder lastMessage = new StringBuilder();
                        foreach(IOffer offer in this.OffersPublished)
                        {
                                string message = offer.GetPeriodTimeOffersAcceptedData(periodTime);
                                if (message != "NonAccepted")
                                {
                                        lastMessage.Append(message);
                                        offersAccepted += 1;
                                }
                        }
                        lastMessage.Append($"En los ultimos {periodTime} días le aceptaron {offersAccepted} ofertas");
                        return Convert.ToString(lastMessage);
                }

                /// <summary>
                /// Remueve palabras clave de una oferta
                /// </summary>
                /// <param name="offer"></param>
                /// <param name="keyWord"></param>
                public void RemoveKeyWords(IOffer offer, string keyWord)
                {
                        offer.KeyWords.Remove(keyWord);
                }

                /// <summary>
                /// Agrega palabras clave a una oferta
                /// </summary>
                /// <param name="offer"></param>
                /// <param name="keyWord"></param>
                public string AddKeyWords(IOffer offer, string keyWord)
                {                
                        if (!offer.KeyWords.Contains(keyWord))
                        {
                                offer.KeyWords.Add(keyWord);
                                return $"Se agrego {keyWord} a la oferta de {offer.Product.Quantity} de {offer.Product.Classification.Category}";
                        }
                        return $"{keyWord} ya se encuntra como palabra clave en la oferta seleccionada";
                }

                /// <summary>
                /// Agrega uhabilitaciones a la oferta
                /// </summary>
                /// <param name="offer"></param>
                /// <param name="qualification"></param>
                public void AddQualification(IOffer offer, Qualifications qualification)
                {
                        offer.Qualifications.Add(qualification);
                }

                /// <summary>
                /// Remueve la habilitacion de una oferta
                /// </summary>
                /// <param name="offer"></param>
                /// <param name="qualification"></param>
                public void RemoveQualification(IOffer offer, Qualifications qualification)
                {
                        offer.Qualifications.Remove(qualification);
                }
        }
}
