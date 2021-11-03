using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace Proyect
{
    /// <summary>
    /// Representa las ofertas constantes de las companias.
    /// </summary>
    public class ConstantOffer : IOffer
    {
        private ProductOffer product;

        private List<Qualifications> qualifications;

        private ArrayList keyWords;

        private List<PurchaseData> purchesedData;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConstantOffer"/>
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="quantity"></param>
        /// <param name="cost"></param>
        /// <param name="ubicacion"></param>
        /// <param name="qualifications"></param>
        /// <param name="keyWords"></param>
        public ConstantOffer(Classification tipo, double quantity, double cost, string ubicacion, List<Qualifications> qualifications, ArrayList keyWords)
        {
            this.product = new ProductOffer(tipo,quantity,cost,ubicacion);
            this.Qualifications = qualifications;
            this.KeyWords = keyWords;
            this.purchesedData = new List<PurchaseData>();

        }

        /// <summary>
        /// Obtiene el producto de una oferta.
        /// </summary>
        /// <value>this.product</value>
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
        public List<PurchaseData> PurchesedData
        {
            get
            {
                return this.purchesedData;
            }
        }

        /// <summary>
        /// Obtiene la informacion de compra de la oferta (por patron expert).
        /// </summary>
        /// <returns>Devuelve un string con la informacion de compra.</returns>
        public string GetPurchesedData()
        {
            bool ofertaAceptada = false;
            StringBuilder message = new StringBuilder();
            foreach(PurchaseData item in this.PurchesedData)
            {
                message.Append($"{this.Product.Quantity} {this.Product.Classification.Category} (Constant offer) Accepted at {item.PurchaseDate} by {item.Buyer.Name}\n");
                ofertaAceptada = true;
            }
            if (!ofertaAceptada)
            {
                message.Append($"{this.Product.Quantity} of {this.Product.Classification.Category} (Constant offer) not Accepted\n");
            }
            return Convert.ToString(message);
        }

        /// <summary>
        /// Obtiene la informacion de compra del ultimo emprendedor que acepta la oferta.
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>Mensaje con la infromacion de compra de la oferta, si la misma entra dentro del rango estipulado, en caso contrario, mensaje que indica dicha situacion.</returns>
        public string GetPeriodTimeOffersAcceptedData(int periodTime)
        {
            StringBuilder message = new StringBuilder();
            PurchaseData lastPurches = this.PurchesedData[this.PurchesedData.Count - 1];
            int diference = Convert.ToInt32(lastPurches.PurchaseDate - DateTime.Now);
            if(diference <= periodTime)
            {
                message.Append($"{this.Product.Quantity} {this.Product.Classification.Category} Accepted at {lastPurches.PurchaseDate} by {lastPurches.Buyer.Name}\n");
                return Convert.ToString(message);
            }
            return "NonAccepte";
        }

        /// <summary>
        /// Obtiene la fecha de compra del emprendedor ingresado (expert).
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns> La fecha de compra del emprendedor ingresado</returns>
        public DateTime GetOfferBuyerTimeData(Emprendedor emprendedor)
        {
            PurchaseData dateBuyerData = this.PurchesedData[0];
            foreach (PurchaseData item in this.PurchesedData)
            {
                if (item.Buyer == emprendedor)
                {
                    dateBuyerData = item;
                }
            }
            return dateBuyerData.PurchaseDate;
        }

        /// <summary>
        /// Agrega un nuevo comprador a la lista de compradores de esta oferta constante.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <param name="timeAccepted"></param>
        public void PutBuyer(Emprendedor emprendedor, DateTime timeAccepted)
        {
            this.PurchesedData.Add(new PurchaseData(emprendedor));
        }
    }
}