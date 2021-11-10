namespace Proyect
{
    /// <summary>
    /// Representa el producto de una oferta (Esta en una relacion de composicion con la clase Offer, siendo product la clase compuesta).
    /// </summary>
    public class ProductOffer
    {
        private Classification classification;
        private double quantity;
        private double price;
        private string ubication;

        /// <summary>
        /// Obtiene un obtiene una instancia de clasificacion valida para un producto.
        /// </summary>
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
        /// Inicializa una nueva instancia de la clase <see cref="ProductOffer"/>.
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

    }
}
