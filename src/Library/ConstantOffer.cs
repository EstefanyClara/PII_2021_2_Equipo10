using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace Proyect
{
    /// <summary>
    /// Representa las ofertas constantes de las companias (Es del tipo IOffer, a quien usa, por lo que cumple con ISP).
    /// </summary>
    public class ConstantOffer : IOffer
    {
        private ProductOffer product;

        private List<Qualifications> qualifications;

        private ArrayList keyWords;

        private List<PurchaseData> purchesedData;

        private string datePublished;

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
            this.datePublished = "Siempre";
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
                if (this.purchesedData.Count == 0)
                {
                    return null;
                }
                else
                {
                    return this.purchesedData;
                }
            }
        }

        /// <summary>
        /// Obtiene la fecha de publicacion de la oferta.
        /// </summary>
        /// <value></value>
        public string DatePublished{get {return this.datePublished;}}

        /// <summary>
        /// Obtiene la informacion de compra del ultimo emprendedor que acepta la oferta (Patron expert).
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>Mensaje con la infromacion de compra de la oferta, si la misma entra dentro del rango estipulado, en caso contrario, mensaje que indica dicha situacion.</returns>
        public bool GetPeriodTimeOffersAcceptedData(int periodTime)
        {
            PurchaseData lastPurches = this.PurchesedData[this.PurchesedData.Count - 1];
            TimeSpan diference = lastPurches.PurchaseDate - DateTime.Now;
            if(Convert.ToDouble(diference.TotalHours) <= periodTime*24)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obtiene la fecha de compra del emprendedor ingresado (Patron expert).
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
        /// Agrega un nuevo comprador a la lista de compradores de esta oferta constante (Se utiliza creator, y expert).
        /// </summary>
        /// <param name="emprendedor"></param>
        public void PutBuyer(Emprendedor emprendedor)
        {
            this.PurchesedData.Add(new PurchaseData(emprendedor));
        }
    }
}