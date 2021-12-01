using System.Collections.Generic;
using System.Collections;
using System;
using System.Text.Json.Serialization;

namespace Proyect
{
    /// <summary>
    /// Representa las ofertas constantes de las companias (Es del tipo IOffer, a quien usa, por lo que cumple con ISP).
    /// </summary>
    public class ConstantOffer : IOffer
    {
        private ProductOffer product;

        private IList<Qualifications> qualifications;

        private ArrayList keyWords;

        private IList<PurchaseData> purchesedData;

        private string datePublished;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConstantOffer"/>
        /// </summary>
        /// <param name="tipo">El tipo del producto a ofertas.</param>
        /// <param name="quantity">La cantidad del producto a ofertas.</param>
        /// <param name="cost">El costo del producto a ofertar.</param>
        /// <param name="ubicacion">La ubicacion del producto a ofertar.</param>
        /// <param name="qualifications">La habilitaciones de la oferta.</param>
        /// <param name="keyWords">Las palabras clave asociadas a la oferta.</param>
        public ConstantOffer(Classification tipo, double quantity, double cost, string ubicacion, List<Qualifications> qualifications, ArrayList keyWords)
        {
            this.product = new ProductOffer(tipo, quantity, cost, ubicacion);
            this.Qualifications = qualifications;
            this.KeyWords = keyWords;
            this.purchesedData = new List<PurchaseData>();
            this.datePublished = DateTime.Now.ToString();
        }

        /// <summary>
        /// Constructor utilizado en la serializacion.
        /// </summary>
        [JsonConstructor]
        public ConstantOffer()
        {

        }

        /// <summary>
        /// Obtiene el producto de una oferta.
        /// </summary>
        /// <value>El producto.</value>
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
        /// <value>Las habilitaciones.</value>
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
        /// <value>La palabras clave.</value>
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
        /// Obtiene la informacion de el o los compardores de esta oferta constante (Informacion de compra).
        /// </summary>
        /// <value>La lista de infromacion de compra.</value>
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
        /// <value>La fecha de publicacion.</value>
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
        /// Obtiene la informacion de compra de todos los emprendedores que aceptaron la oferta en el tiempo estipulado(Patron expert).
        /// Esta operacion es polimorfica.
        /// </summary>
        /// <param name="periodTime">El periodo de tiempo.</param>
        /// <returns>Mensaje con la infromacion de compra de la oferta, si la misma entra dentro del rango estipulado, en caso contrario, mensaje que indica dicha situacion.</returns>
        public IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime)
        {
            List<PurchaseData> infoCompras = new List<PurchaseData>();
            foreach (PurchaseData item in this.PurchesedData)
            {
                TimeSpan diference = item.PurchaseDate - DateTime.Now;
                if (Convert.ToDouble(diference.TotalHours) <= periodTime * 24)
                {
                    infoCompras.Add(item);
                }
            }
            return infoCompras;
        }

        /// <summary>
        /// Obtiene la informacion de compra del emprendedor especificado en el tiempo ingresado (Por expert).
        /// Esta operacion es polimorfica.
        /// </summary>
        /// <param name="periodTime">El periodo de tiempo.</param>
        /// <param name="emprendedor">Emprendedor.</param>
        /// <returns></returns>
        public IList<PurchaseData> GetPeriodTimeOffersAcceptedData(int periodTime, Emprendedor emprendedor)
        {
            List<PurchaseData> infoCompras = new List<PurchaseData>();
            foreach (PurchaseData item in this.PurchesedData)
            {
                TimeSpan diference = item.PurchaseDate - DateTime.Now;
                if (Convert.ToDouble(diference.TotalHours) <= periodTime * 24 & item.Buyer.Equals(emprendedor))
                {
                    infoCompras.Add(item);
                }
            }
            return infoCompras;
        }

        /// <summary>
        /// Obtiene la informacion de compra del emprendedor especificado (Por expert).
        /// </summary>
        /// <param name="emprendedor">El emprendedor.</param>
        /// <returns>La lista con la informacion de compra.</returns>
        public IList<PurchaseData> GetEntrepreneursPurchaseData(Emprendedor emprendedor)
        {
            List<PurchaseData> infoCompras = new List<PurchaseData>();
            foreach (PurchaseData item in this.PurchesedData)
            {
                if (item.Buyer.Equals(emprendedor))
                {
                    infoCompras.Add(item);
                }
            }
            return infoCompras;
        }

        /// <summary>
        /// Agrega un nuevo comprador a la lista de compradores de esta oferta constante (Se utiliza creator, y expert).
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
            this.purchesedData.Add(new PurchaseData(emprendedor));
            return true;
        }
    }
}