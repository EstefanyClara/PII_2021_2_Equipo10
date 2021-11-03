using System;

namespace Proyect
{
    /// <summary>
    /// Clase para guardar la fecha y quien compró las ofertas.
    /// </summary>
    public class PurchaseData
    {
        private Emprendedor buyer;
        private DateTime purchaseDate;

        /// <summary>
        /// Metodo get del comprador.
        /// </summary>
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
        /// Metodo get de la fecha de compra.
        /// </summary>
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
        /// <param name="buyer"></param>
        public PurchaseData(Emprendedor buyer)
        {
            this.Buyer = buyer;
            this.PurchaseDate = DateTime.Now;
        }
    }
}