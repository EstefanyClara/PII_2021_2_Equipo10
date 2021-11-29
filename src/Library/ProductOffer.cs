using System.Text.Json;
using System.Text.Json.Serialization;
using System;


namespace Proyect
{
    /// <summary>
    /// Representa el producto de una oferta (Esta en una relacion de composicion con la clase Offer, siendo product la clase compuesta).
    /// </summary>
    [Serializable]
    public class ProductOffer : IJsonConvertible
    {
        private Classification classification;
        private double quantity;
        private double price;
        private string ubication;

        /// <summary>
        /// Obtiene un obtiene una instancia de clasificacion valida para un producto.
        /// </summary>
        [JsonInclude]
        public Classification Classification
        {
            get
            {
                return this.classification;
            }
            set
            {
                this.classification = value;
            }
        }
        /// <summary>
        /// Determina la cantidad de unidades de un producto.
        /// </summary>
        [JsonInclude]
        public double Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                this.quantity = value;
            }
        }
        /// <summary>
        /// Determina el precio por unidad de producto.
        /// </summary>
        [JsonInclude]
        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }
        /// <summary>
        /// Determina la ubicacion de un producto.
        /// </summary>
        [JsonInclude]
        public string Ubication
        {
            get
            {
                return this.ubication;
            }
            set
            {
                this.ubication = value;
            }
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProductOffer"/>
        /// </summary>
        /// <param name="classification">Clasificacion del producto.</param>
        /// <param name="quantity">Cantidad del producto.</param>
        /// <param name="price">Precio del producto.</param>
        /// <param name="ubication">Ubicacion del producto.</param>
        public ProductOffer(Classification classification, double quantity, double price, string ubication)

        {
            this.Classification = classification;
            this.Quantity = quantity;
            this.Price = price;
            this.Ubication = ubication; 
        }

        /// <summary>
        /// jjj
        /// </summary>
        [JsonConstructor]
        public ProductOffer()
        {
        }

        /// <summary>
        /// kkk
        /// </summary>
        /// <returns></returns>
        public string ConvertToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
