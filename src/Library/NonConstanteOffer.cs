using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

namespace Proyect
{
    /// <summary>
    /// Esta clase representa las ofertas constantes de las companias (Cumple con ISP).
    /// </summary>
    public class NonConstantOffer : IOffer
    {
        private ProductOffer product;

        private List<Qualifications> qualifications;

        private ArrayList keyWords;

        private List<PurchaseData> purchesedData;

        private string datePublished;

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
            this.purchesedData = new List<PurchaseData>();
            this.Qualifications = qualifications;
            this.KeyWords = keyWords;
            this.datePublished = Convert.ToString(DateTime.Now);
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
        public List<PurchaseData> PurchesedData
        {
            get
            {
                return this.purchesedData;
            }
        }

        /// <summary>
        /// Obtiene la fecha de publicacion de la oferta.
        /// </summary>
        /// <value>dateTime</value>
        public string DatePublished{get {return this.datePublished;}}

        /// <summary>
        /// Obtiene la informacion de compra de la oferta (expert).
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns>si la oferta se compro antes de la fecha estipulada, devuelve la iformacion de compra, en caso contrario, devuelve un striing indicando dicha situacion</returns>
        public List<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime)
        {
            TimeSpan diference = this.PurchesedData[0].PurchaseDate - DateTime.Now;
            if(Convert.ToDouble(diference.TotalHours) <= periodTime*24)
            {
                return this.PurchesedData;
            }
            return new List<PurchaseData>();
        }

        /// <summary>
        /// Obtiene la informacion de compra del emprendedor especificado, en el tiempo especificado.
        /// </summary>
        /// <param name="periodTime"></param>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        public List<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime, Emprendedor emprendedor)
        {
            TimeSpan diference = this.PurchesedData[0].PurchaseDate - DateTime.Now;
            if(Convert.ToDouble(diference.TotalHours) <= periodTime*24)
            {
                return this.PurchesedData;
            }
            return new List<PurchaseData>();
        }

        /// <summary>
        /// Coloca el emprendedor y la fecha de compra, en la informacion de compra.
        /// </summary>
        /// <param name="emprendedor"></param>
        public bool PutBuyer(Emprendedor emprendedor)
        {
            foreach(Qualifications item in this.Qualifications)
            {
                if(!emprendedor.Qualifications.Contains(item))
                {
                    return false;
                }
            }
            if (this.PurchesedData.Count == 0)
            {
                this.PurchesedData.Add(new PurchaseData(emprendedor));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obtiene la infromacion de compra de un emprendedor.
        /// </summary>
        /// <param name="emprendedor"></param>
        /// <returns></returns>
        public List<PurchaseData> GetEntrepreneursPurchaseData(Emprendedor emprendedor)
        {
            List<PurchaseData> compra = new List<PurchaseData>();
            foreach(PurchaseData item in this.PurchesedData)
            {
                if (item.Buyer.Equals(emprendedor))
                {
                    compra.Add(item);
                }
            }
            return compra;
        }
    }
}
