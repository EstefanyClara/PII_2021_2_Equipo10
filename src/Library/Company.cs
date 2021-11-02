using System; 
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Proyect
{
        /// <summary>
        /// Esta clase representa la compania. Hereda de User. 
        /// </summary>
        public class Company: User
        {
                /// <summary>
                /// Lista de ofertas de la compania.
                /// </summary>
                private List<Offer> offers = new List<Offer>(); 

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
                /// <value></value>
                public List<Offer> OffersPublished
                {
                        get{ return this.offers; }     
                }

                /// <summary>
                /// Publica una oferta,es decir,la crea y la guarda en su lista.
                /// </summary>
                public  void PublicOffer(bool ifConstant, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
                {
                        OffersPublished.Add(new Offer(ifConstant, tipo, quantity, cost, ubication, qualifications, keyWords));
                }

                /// <summary>
                /// Remueve ofertas.
                /// </summary>
                /// <param name="offer">Oferta.</param>
                public void RemoveOffer(Offer offer)
                {
                        if (this.OffersPublished.Contains(offer))
                        {
                                this.OffersPublished.Remove(offer);
                        }
                }

                /// <summary>
                /// Obtiene un string con las ofertas que fueron aceptadas por emprendedores.
                /// </summary>
                /// <returns>Las ofertas aceptadas.</returns>
                public string GetOffersAccepted()
                {
                        StringBuilder message = new StringBuilder();
                        foreach (Offer item in this.OffersPublished)
                        {
                                // Cambio buyer por purchaseData, hay que testear
                                if (item.PurchaseData != null)
                                {
                                        message.Append($"{item.Product.Quantity} {item.Product.Classification.Category} Accepted at {item.PurchaseData.PurchaseDate}\n");
                                }
                                else 
                                {
                                        message.Append($"{item.Product.Quantity} of {item.Product.Classification.Category} not Accepted\n");
                                }
                        }
                        return Convert.ToString(message);
                }

                /// <summary>
                /// Obtiene la cantidad de ofertas que publico la compania, que fueron aceptadas.
                /// </summary>
                /// <param name="periodTime">Periodo de tiempo.</param>
                /// <returns>Las ofertas aceptadas en un periodo de tiempo.</returns>
                public int GetPeriodTimeOffersAccepted(int periodTime)
                {
                        int offersAccepted = 0;
                        foreach(Offer offer in this.OffersPublished)
                        {
                                // Cambio buyer por purchaseData, hay que testear
                                if (offer.PurchaseData != null)
                                {
                                        int diference = Convert.ToInt32(offer.PurchaseData.PurchaseDate - DateTime.Now);
                                        if(diference <= periodTime)
                                        {
                                                offersAccepted += 1;
                                        }
                                }
                        }
                        return offersAccepted;
                }

                /// <summary>
                /// Remueve palabras clave de una oferta.
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="keyWord">La palabra clave de la oferta.</param>
                public void RemoveKeyWords(Offer offer, string keyWord)
                {
                        if (this.OffersPublished.Contains(offer))
                        {
                                if (offer.KeyWords.Contains(keyWord))
                                {
                                        offer.KeyWords.Remove(keyWord);
                                }
                        }
                }

                /// <summary>
                /// Agrega palabras clave a una oferta.
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="keyWord">La palabra clave de la oferta.</param>
                public void AddKeyWords(Offer offer, string keyWord)
                {
                        if (!this.OffersPublished.Contains(offer))
                        {
                                if (!offer.KeyWords.Contains(keyWord))
                                {
                                        offer.KeyWords.Add(keyWord);
                                }
                        }
                }

                /// <summary>
                /// Agrega habilitaciones a la oferta.
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="qualification">La habilitaciones de la oferta.</param>
                public void AddQualification(Offer offer, Qualifications qualification)
                {
                        if (this.OffersPublished.Contains(offer))
                        {
                                if (!offer.Qualifications.Contains(qualification))
                                {
                                        offer.Qualifications.Add(qualification);
                                }
                        }
                }

                /// <summary>
                /// Remueve la habilitacion de una oferta.
                /// </summary>
                /// <param name="offer">La oferta.</param>
                /// <param name="qualification">La habilitacion de la oferta.</param>
                public void RemoveQualification(Offer offer, Qualifications qualification)
                {
                        if (this.OffersPublished.Contains(offer))
                        {
                                if (offer.Qualifications.Contains(qualification))
                                {
                                        offer.Qualifications.Remove(qualification);
                                }
                        }
                }
        }
}
