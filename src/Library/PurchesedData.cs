using System;

namespace Proyect
{
    /// <summary>
    /// Clase que representa la informacion de compra de una oferta
    /// </summary>
    public class PurchesedData
    {
        public DateTime TimeAccepted;

        public Emprendedor Buyer;

        /// <summary>
        /// Constructor de PurchesedData
        /// </summary>
        public PurchesedData(Emprendedor emprendedor, DateTime timeAccepted)
        {
            this.TimeAccepted = timeAccepted;
            this.Buyer = emprendedor;
        }
    }
}