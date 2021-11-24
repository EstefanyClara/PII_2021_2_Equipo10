using System;

namespace Proyect
{
    /// <summary>
    /// Clase para guardar la fecha y quien compró las ofertas
    /// </summary>
    public class PurchaseData
    {
        private Emprendedor buyer;
        private DateTime purchaseDate;

        /// <summary>
        /// Metodo get del comprador
        /// </summary>
        /// <value></value>
        public Emprendedor Buyer
        {
            get{return this.buyer;}
            set{this.buyer = value;}
        }

        /// <summary>
        /// Metodo get de la fecha de compra
        /// </summary>
        /// <value></value>
        public DateTime PurchaseDate
        {
            get{return this.purchaseDate;}
            set{this.purchaseDate = value;}
        }

        /// <summary>
        /// Constructor del registro de compra
        /// </summary>
        /// <param name="buyer"></param>
        public PurchaseData(Emprendedor buyer)
        {
            this.Buyer = buyer;
            this.PurchaseDate = DateTime.Now;
        }
    }
}