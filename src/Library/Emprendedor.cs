using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Proyect
{
    /// <summary>
    /// Clase emprendedor. 
    /// </summary>
    public class Emprendedor : User
    {
        private List<Qualifications> qualifications;

        private ArrayList specializations;

        private List<IOffer> purchasedOffer;
        /// <summary>
        /// Constructor de emprendedor.
        /// </summary>
        /// <param name="name">Nombre del emprendedor.</param>
        /// <param name="ubication">Ubicacion del emprendedor.</param>
        /// <param name="rubro">Rubro del emprendedor.</param>
        /// <param name="qualifications">Hablitaciones del emprendedor.</param>
        /// <param name="specializations">Especializaciones del emprendedor.</param>
        /// <returns></returns>
        public Emprendedor(string name, string ubication, Rubro rubro, List<Qualifications> qualifications, ArrayList specializations):base(name,ubication,rubro)
        {
            this.Qualifications = qualifications;
            this.Specializations = specializations;
            this.purchasedOffer = new List<IOffer>();
        }
        /// <summary>
        /// Propiedad get y set de las habilitaciones.
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
        /// Propiedad Specializations.
        /// </summary>
        /// <value></value>
        public ArrayList Specializations
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
        /// Obtiene la lista de ofertas ofertas aceptadas por el emprendedor.
        /// </summary>
        /// <value></value>
        public List<IOffer> PurchasedOffers
        {
            get
            {
                return this.purchasedOffer;
            }
        }

        /// <summary>
        /// Metodo para agregar una oferta a la lista de ofertas que el emprendedor acepto.
        /// </summary>
        public void AddPurchasedOffer(IOffer offer)
        {
            this.purchasedOffer.Add(offer);
        }

        /// <summary>
        /// Obtiene un string indicando las ofertas que fueron aceptadas por el por el emprendedor, junto con algunos datos.
        /// </summary>
        /// <returns></returns>
        public string GetOffersAccepted()
        {
            StringBuilder message = new StringBuilder();
            foreach (IOffer item in this.PurchasedOffers)
            {
                message.Append($"{item.Product.Quantity} {item.Product.Classification.Category} at a price of {item.Product.Price}$ Accepted at {item.GetOfferBuyerTimeData(this)}\n");
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en un periodo de tiempo.
        /// </summary>
        /// <param name="periodTime">Periodo de tiempo.</param>
        /// <returns></returns>
        public string GetPeriodTimeOffersAccepted(int periodTime)
        {
            StringBuilder message = new StringBuilder();
            int offersAccepted = 0;
            foreach(IOffer offer in this.PurchasedOffers)
            {
                DateTime fecha = offer.GetOfferBuyerTimeData(this);
                int diference = Convert.ToInt32(fecha - DateTime.Now);
                if(diference <= periodTime)
                {
                    message.Append($"{offer.Product.Quantity} {offer.Product.Classification.Category} at a price of {offer.Product.Price}$ Accepted at {offer.GetOfferBuyerTimeData(this)}\n");
                    offersAccepted += 1;
                }
            }
            message.Append($"Usted ah aceptado {offersAccepted} ofertas en los ultimos {periodTime} días");
            return Convert.ToString(message);
        }
    }
}