using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace Proyect
{
    /// <summary>
    /// Esta clase representa las ofertas constantes de las companias..
    /// </summary>
    public class NonConstantOffer : IOffer
    {
        private ProductOffer product;

        private List<Qualifications> qualifications;

        private ArrayList keyWords;

        private PurchaseData purchesedData;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="NonConstantOffer"/>
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="quantity"></param>
        /// <param name="cost"></param>
        /// <param name="ubicacion"></param>
        /// <param name="qualifications"></param>
        /// <param name="keyWords"></param>
        public NonConstantOffer(Classification tipo, double quantity, double cost, string ubicacion, List<Qualifications> qualifications, ArrayList keyWords)
        {
            this.product = new ProductOffer(tipo,quantity,cost,ubicacion);
            this.Qualifications = qualifications;
            this.KeyWords = keyWords;
        }

        /// <summary>
        /// Obtiene el producto de una oferta.
        /// </summary>
        /// <value></value>
        public ProductOffer Product
        {
            get
            {
                return this.product;
            }
        }

        /// <summary>
        /// Obtiene la lista de las habilitaciones de una oferta.
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
        /// Obtiene la lista de palbras clave de la oferta.
        /// </summary>
        /// <value></value>
        public ArrayList KeyWords
        {
            get
            {
                return this.keyWords;
            }
            set
            {
                this.keyWords = value;
            }
        }

        /// <summary>
        /// Obtiene la informacion de el o los compardores de esta oferta constante.
        /// </summary>
        /// <value></value>
        public PurchaseData PurchesedData
        {
            get
            {
                return this.purchesedData;
            }
            set
            {
                this.purchesedData = value;
            }
        }

        /// <summary>
        /// Obtiene la informacion de compra de la oferta (por patron expert).
        /// </summary>
        /// <returns>Un string con la informacion de quien lo compro.</returns>
        public string GetPurchesedData()
        {
            StringBuilder message = new StringBuilder();
            if (this.PurchesedData != null)
            {
                message.Append($"{this.Product.Quantity} {this.Product.Classification.Category} Accepted at {this.PurchesedData.PurchaseDate} by {this.PurchesedData.Buyer}\n");
            }
            else
            {
                message.Append($"{this.Product.Quantity} of {this.Product.Classification.Category} not Accepted\n");
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Obtiene la informacion de compra de la oferta (expert).
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>si la oferta se compro antes de la fecha estipulada, devuelve la iformacion de compra, en caso contrario, devuelve un striing indicando dicha situacion</returns>
        public string GetPeriodTimeOffersAcceptedData(int periodTime)
        {
            StringBuilder message = new StringBuilder();
            int diference = Convert.ToInt32(this.PurchesedData.PurchaseDate - DateTime.Now);
            if(diference <= periodTime)
            {
                message.Append($"{this.Product.Quantity} {this.Product.Classification.Category} Accepted at {this.PurchesedData.PurchaseDate} by {this.PurchesedData.Buyer}\n");
                return Convert.ToString(message);
            }
            return "NonAccepte";
        }

        /// <summary>
        /// Obtiene la fecha de compra del emprendedor ingresado.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns>retorna la fecha de cmpra de la oferta</returns>
        public DateTime GetOfferBuyerTimeData(Emprendedor emprendedor)
        {
            if (this.PurchesedData.Buyer != emprendedor)
            {
                return DateTime.Now;
            }
            return this.PurchesedData.PurchaseDate;
        }

        /// <summary>
        /// Coloca el emprendedor y la fecha de compra, en la informacion de compra.
        /// </summary>
        /// <param name="emprendedor"></param>
        public void PutBuyer(Emprendedor emprendedor)
        {
            this.PurchesedData = new PurchaseData(emprendedor);
        }
    }
}
