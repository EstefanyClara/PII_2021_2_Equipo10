using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Proyect
{
    /// <summary>
    /// Clase emprendedor.
    /// </summary>
    public class Emprendedor : User
    {
        private List<Qualifications> qualifications;

        private List<Qualifications> specializations;

        private List<Offer> purchasedOffer;
/// <summary>
/// Constructor de emprendedor
/// </summary>
/// <param name="name"></param>
/// <param name="ubication"></param>
/// <param name="rubro"></param>
/// <param name="qualifications"></param>
/// <param name="specializations"></param>
/// <returns></returns>
        public Emprendedor(string name, string ubication, Rubro rubro, List<Qualifications> qualifications, List<Qualifications> specializations):base(name,ubication,rubro)
        {
            this.Qualifications = qualifications;
            this.Specializations = specializations;
            this.purchasedOffer = new List<Offer>();
        }
/// <summary>
/// Propiedad get y set de las habilitaciones
/// </summary>
/// <value></value>
        public List<Qualifications> Qualifications
        {
            get
            {
                return this.qualifications;
            }
            set
            {
                this.qualifications = value;
            }
        }
/// <summary>
/// Propiedad Specializations
/// </summary>
/// <value></value>
        public List<Qualifications> Specializations
        {
            get
            {
                return this.specializations;
            }
            set
            {
                this.specializations = value;
            }
        }

        /// <summary>
        /// Obtiene la lista de ofertas ofertas aceptadas por el emprendedor
        /// </summary>
        /// <value></value>
        public List<Offer> PurchasedOffers
        {
            get
            {
                return this.purchasedOffer;
            }
        }

        /// <summary>
        /// Metdod para agregar una oferta a la lista de ofertas que el emprendedor acepto
        /// </summary>
        public void AddPurchasedOffer(Offer offer)
        {
            this.purchasedOffer.Add(offer);
        }

        /// <summary>
        /// Obtiene un string indicando las ofertas que fueron aceptadas por el por el emprendedor, junto con algunos datos
        /// </summary>
        /// <returns></returns>
        public string GetOffersAccepted()
        {
            StringBuilder message = new StringBuilder();
            foreach (Offer item in this.PurchasedOffers)
            {    
                message.Append($"{item.Product.Quantity} {item.Product.Classification.Category} at a price of {item.Product.Price}$ Accepted at {item.TimeAccepted}\n");
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en eun periodo de tiempo
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns></returns>
        public int GetPeriodTimeOffersAccepted(int periodTime)
        {
            int offersAccepted = 0;
            foreach(Offer offer in this.PurchasedOffers)
            {
                if (offer.Buyer != null)
                {
                    int diference = Convert.ToInt32(offer.TimeAccepted - DateTime.Now);
                    if(diference <= periodTime)
                    {
                        offersAccepted += 1;
                    }
                }
            }
            return offersAccepted;
        }
    }
}