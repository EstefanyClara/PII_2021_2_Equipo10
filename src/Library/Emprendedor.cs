using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Esta clase representa un Emprendedor, hereda de user (Tienen relacion taxonomica, cumple con el ISP). 
    /// </summary>
    public class Emprendedor : User
    {
        private IList<Qualifications> qualifications;

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
        /// <param name="user_Contact">Contacto del emprendedor.</param>
        public Emprendedor(string user_Id, string name, string ubication, Rubro rubro,string user_Contact, List<Qualifications> qualifications, string specializations):base(user_Id, name, ubication, rubro, user_Contact)
        {
            this.Qualifications = qualifications;
            this.Specializations = specializations;
            this.purchasedOffer = new List<IOffer>();
        }
        /// <summary>
        /// Propiedad get y set de las habilitaciones.
        /// </summary>
        /// <value>Las habiliatciones.</value>
        public IList<Qualifications> Qualifications
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
        /// Propiedad de get y set de las especializaciones.
        /// </summary>
        /// <value>La especializaciones.</value>
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
        /// <value>tLa lista de ofertas que el emprendeodr acepto.</value>
        public List<IOffer> PurchasedOffers
        {
            get
            {
                return this.purchasedOffer;
            }
            set
            {
                this.purchasedOffer = value;
            }
        }

        /// <summary>
        /// Metodo para agregar una oferta a la lista de ofertas que el emprendedor acepto (Por expert).
        /// </summary>
        /// <param name="offer">La oferta a agregar.</param>
        public void AddPurchasedOffer(IOffer offer)
        {
            this.purchasedOffer.Add(offer);
        }

        /// <summary>
        /// Obtiene una lista de ofertas que fueron aceptadas en un periodo de tiempo (Por Expert).
        /// </summary>
        /// <param name="periodTime">Periodo de tiempo.</param>
        /// <returns>La lista de ofertas.</returns>
        public List<IOffer> GetOffersAccepted(int periodTime)
        {
            List<IOffer> ofertas = new List<IOffer>();
            foreach(IOffer offer in this.PurchasedOffers)
            {
                IList<PurchaseData> infoDeCompraOferta = offer.GetPeriodTimeOffersAcceptedData(periodTime, this);
                if(infoDeCompraOferta.Count >= 1)
                {
                    ofertas.Add(offer);
                }
            }
            return ofertas;
        }
    }
}