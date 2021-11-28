using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Representa las ofertas constantes de las companias (Es del tipo IOffer, a quien usa, por lo que cumple con ISP).
    /// </summary>
    public class ConstantOffer : IOffer
    {
        private static int id = 0;
        private int ID;
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
            this.ID = ++id;
        }

        /// <summary>
        /// Id por el cual se va a identificar la oferta dentro de nuestro programa.
        /// </summary>
        /// <value></value>
        public int Id
        {
            get{return this.ID;}
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
        public bool GetPeriodTimeOffersAcceptedData(int periodTime, out PurchaseData tiempo)
        {
            PurchaseData lastPurches = this.PurchesedData[this.PurchesedData.Count - 1];
            if (periodTime != -1)
            {
                TimeSpan diference = lastPurches.PurchaseDate - DateTime.Now;
                if(Convert.ToDouble(diference.TotalHours) <= periodTime*24)
                {
                    tiempo = lastPurches;
                    return true;
                }
                tiempo = null;
                return false;
            }else
            {
                tiempo = lastPurches;
                return true;
            }
        }

        /// <summary>
        /// Agrega un nuevo comprador a la lista de compradores de esta oferta constante (Se utiliza creator, y expert).
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
            this.purchesedData.Add(new PurchaseData(emprendedor));
            return true;
        }
    }
}