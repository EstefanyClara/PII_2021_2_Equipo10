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
                /// <param name="userChat_Id">Id de la compania.</param>
                public Company(string userChat_Id, string name, string ubication, Rubro rubro):base(name, ubication, rubro, userChat_Id)
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
                        OffersPublished.Add(new NonConstantOffer(tipo, quantity, cost, ubication, qualifications, keyWords));
                }

                /// <summary>
                /// Remueve ofertas (Por Expert).
                /// </summary>
                /// <param name="offer">Oferta.</param>
                public void RemoveOffer(IOffer offer)
                {
                        this.OffersPublished.Remove(offer);
                }

                /// <summary>
                /// Obtiene un string con todas las ofertas que fueron o no fueron aceptadas por emprendedores(por expert le asignamos esta responsabilidad).
                /// Es una operacion polimorfica.
                /// </summary>
                /// <returns>Las ofertas aceptadas.</returns>
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
                /// Obtiene la cantidad de ofertas que publico la compania, que fueron aceptadas en un periodo de tiempo (Por expert).
                /// Es una operwacion polimorfica.
                /// </summary>
                /// <param name="periodTime">Periodo de tiempo.</param>
                /// <returns>Las ofertas aceptadas en un periodo de tiempo.</returns>
                public string GetOffersAccepted(int periodTime)
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
                /// Remueve palabras clave de una oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="keyWord">La palabra clave de la oferta.</param>
                public void RemoveKeyWords(IOffer offer, string keyWord)
                {
                        offer.KeyWords.Remove(keyWord);
                }

                /// <summary>
                /// Agrega palabras clave a una oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="keyWord">La palabra clave de la oferta.</param>
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
                /// Agrega habilitaciones a la oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="qualification">La habilitaciones de la oferta.</param>
                public void AddQualification(IOffer offer, Qualifications qualification)
                {
                        offer.Qualifications.Add(qualification);
                }

                /// <summary>
                /// Remueve la habilitacion de una oferta (Por expert).
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="qualification">La habilitacion de la oferta.</param>
                public void RemoveQualification(IOffer offer, Qualifications qualification)
                {
                        offer.Qualifications.Remove(qualification);
                }
        }
}
