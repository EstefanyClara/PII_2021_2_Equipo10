using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Proyect
{
    /// <summary>
    /// Esta clase representa un Emprendedor, hereda de user (Tienen relaciontaxonomica). 
    /// </summary>
    public class Emprendedor : User
    {
        private List<Qualifications> qualifications;

        private string specializations;

        private List<IOffer> purchasedOffer;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Emprendedor"/>
        /// </summary>
        /// <param name="name">Nombre del emprendedor.</param>
        /// <param name="ubication">Ubicacion del emprendedor.</param>
        /// <param name="rubro">Rubro del emprendedor.</param>
        /// <param name="qualifications">Hablitaciones del emprendedor.</param>
        /// <param name="specializations">Especializaciones del emprendedor.</param>
        /// <param name="user_Id">Identificacion del emprendedor.</param>
        public Emprendedor(string user_Id, string name, string ubication, Rubro rubro,string user_Contact, List<Qualifications> qualifications, string specializations):base(user_Id, name, ubication, rubro, user_Contact)
        {
            this.Qualifications = qualifications;
            this.Specializations = specializations;
            this.purchasedOffer = new List<IOffer>();
        }
        /// <summary>
        /// Propiedad get y set de las habilitaciones.
        /// </summary>
        /// <value>this.qualifications</value>
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
        /// <value>this.specializations</value>
        public string Specializations
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
        /// <value>this.purchasedOffer</value>
        public List<IOffer> PurchasedOffers
        {
            get
            {
                return this.purchasedOffer;
            }
        }

        /// <summary>
        /// Metodo para agregar una oferta a la lista de ofertas que el emprendedor acepto (Por expert).
        /// </summary>
        /// <param name="offer"></param>
        public void AddPurchasedOffer(IOffer offer)
        {
            this.purchasedOffer.Add(offer);
        }

        /// <summary>
        /// Obtiene la cantidad de ofertas que fueron aceptadas en un periodo de tiempo (Expert).
        /// Es una operacion polimorfica.
        /// </summary>
        /// <param name="periodTime">Periodo de tiempo.</param>
        /// <returns>message</returns>
        public List<IOffer> GetOffersAccepted(int periodTime)
        {
            List<IOffer> ofertas = new List<IOffer>();
            foreach(IOffer offer in this.PurchasedOffers)
            {
                List<PurchaseData> infoDeCompraOferta = offer.GetPeriodTimeOffersAcceptedData(periodTime, this);
                if(infoDeCompraOferta.Count >= 1)
                {
                    ofertas.Add(offer);
                }
            }
            return ofertas;
        }
    }
}