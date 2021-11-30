using System;

namespace Proyect
{
    /// <summary>
    /// Clase para guardar la fecha y quien compró las ofertas (Por SRP), en defnitiva, la informacion de compra.
    /// Esta en una relacion de composicion con la clases IOffer, siendo est ala clase componente.
    /// </summary>
    public class PurchaseData
    {
        private Emprendedor buyer;
        private DateTime purchaseDate;

        /// <summary>
        /// El emprendedor que compro una oferta.
        /// </summary>
        /// <value>El comprador.</value>
        public Emprendedor Buyer
        {
            get
            {
                return this.buyer;
            }
            set
            {
                this.buyer = value;
            }
        }

        /// <summary>
        /// Fecha de compra de la oferta.
        /// </summary>
        /// <value>La fecha de compra.</value>
        public DateTime PurchaseDate
        {
            get
            {
                return this.purchaseDate;
            }
            set
            {
                this.purchaseDate = value;

            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PurchaseData"/>
        /// </summary>
        /// <param name="buyer">El comprador de la oferta.</param>
        public PurchaseData(Emprendedor buyer)
        {
            this.Buyer = buyer;
            this.PurchaseDate = DateTime.Now;
        }
    }
}