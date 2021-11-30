using System.Collections.Generic;
using System.Collections;
using System;
using System.Text.Json.Serialization;

namespace Proyect
{
    /// <summary>
    /// Esta clase representa las ofertas no constantes de las companias (Cumple con ISP).
    /// </summary>
    public class NonConstantOffer : IOffer
    {
        private ProductOffer product;
        private IList<Qualifications> qualifications;
        private ArrayList keyWords;
        private IList<PurchaseData> purchesedData;
        private string datePublished;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="NonConstantOffer"/>
        /// </summary>
        /// <param name="tipo">El tipo del producto a ofertas.</param>
        /// <param name="quantity">La cantidad del producto a ofertas.</param>
        /// <param name="cost">El costo del producto a ofertar.</param>
        /// <param name="ubicacion">La ubicacion del producto a ofertar.</param>
        /// <param name="qualifications">La habiliatciones de la oferta.</param>
        /// <param name="keyWords">Las palabras clave asociadas a la oferta.</param>
        public NonConstantOffer(Classification tipo, double quantity, double cost, string ubicacion, List<Qualifications> qualifications, ArrayList keyWords)
        {
            this.product = new ProductOffer(tipo, quantity, cost, ubicacion);
            this.Qualifications = qualifications;
            this.purchesedData = new List<PurchaseData>();
            this.KeyWords = keyWords;
            this.datePublished = Convert.ToString(DateTime.Now);
        }


        /// <summary>
        /// Constructor utilizado en la serilizacion.
        /// </summary>
        [JsonConstructor]
        public NonConstantOffer()
        {
        }

        /// <summary>
        /// Obtiene el producto de una oferta.
        /// </summary>
        /// <value>Producto.</value>
        [JsonInclude]
        public ProductOffer Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
            }
        }

        /// <summary>
        /// Obtiene la lista de las habilitaciones de una oferta.
        /// </summary>
        /// <value>Lista de habiliatciones.</value>
        [JsonInclude]
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
        /// Obtiene la lista de palbras clave de la oferta.
        /// </summary>
        /// <value>Lista de palabras calve.</value>
        [JsonInclude]
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
        /// <value>Lista de informacion de compra.</value>
        public IList<PurchaseData> PurchesedData
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
        /// Obtiene la fecha de publicacion de la oferta.
        /// </summary>
        /// <value>Fecha de compra.</value>
        public string DatePublished
        {
            get
            {
                return this.datePublished;
            }
            set
            {
                this.datePublished = value;
            }
        }


        /// <summary>
        /// Obtiene la informacion de compra de la oferta, en el tiempo indicado (Por expert).
        /// Esta operacion es polimorfica.
        /// </summary>
        /// <param name="periodTime">El periodo de tiempo.</param>
        /// <returns>Si la oferta se compro antes de la fecha estipulada, devuelve la iformacion de compra, en caso contrario, devuelve un string indicando dicha situacion</returns>
        public IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime)
        {
            TimeSpan diference = this.PurchesedData[0].PurchaseDate - DateTime.Now;
            if (Convert.ToDouble(diference.TotalHours) <= periodTime * 24)
            {
                return this.PurchesedData;
            }
            return new List<PurchaseData>();
        }

        /// <summary>
        /// Obtiene la informacion de compra del emprendedor especificado, en el tiempo especificado (Por expert).
        /// Esta operacion es polimorfica.
        /// </summary>
        /// <param name="periodTime">El periodo de tiempo.</param>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>La lisat de informacion de compra.</returns>
        public IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime, Emprendedor emprendedor)
        {
            TimeSpan diference = this.PurchesedData[0].PurchaseDate - DateTime.Now;
            if (Convert.ToDouble(diference.TotalHours) <= periodTime * 24)
            {
                return this.PurchesedData;
            }
            return new List<PurchaseData>();
        }

        /// <summary>
        /// Coloca el emprendedor y la fecha de compra, en la informacion de compra (Por expert).
        /// </summary>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>Mensaje de confirmacion.</returns>
        public bool PutBuyer(Emprendedor emprendedor)
        {
            foreach (Qualifications item in this.Qualifications)
            {
                foreach (Qualifications value in emprendedor.Qualifications)
                {
                    if (value.QualificationName == item.QualificationName)
                    {
                        break;
                    }
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
        /// Obtiene la infromacion de compra de un emprendedor en esepcifico (Por expert).
        /// </summary>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>La lista de informacíon de compra.</returns>
        public IList<PurchaseData> GetEntrepreneursPurchaseData(Emprendedor emprendedor)
        {
            List<PurchaseData> compra = new List<PurchaseData>();
            foreach (PurchaseData item in this.PurchesedData)
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
