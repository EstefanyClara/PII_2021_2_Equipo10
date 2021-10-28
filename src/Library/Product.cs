using System;

namespace Proyect
{
    /// <summary>
    /// Es el producto de la oferta
    /// </summary>
    public class Product
    {
        private Classification classification;
        private int quantity;
        private double cost;
        private string ubication;

        /// <summary>
        /// Devuelve la clasificacion de ese producto
        /// </summary>
        /// <value></value>
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
        /// Devuelve la cantidad del producto
        /// </summary>
        /// <value></value>
        public int Quantity
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
        /// Devuelve el costo del producto
        /// </summary>
        /// <value></value>
        public double Cost
        {
            get
            {
                return this.cost;
            }
            set
            {
                this.cost = value;
            }
        }

        /// <summary>
        /// Devuelve la ubicacion del producto
        /// </summary>
        /// <value></value>
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
        /// Constructor del producto
        /// </summary>
        /// <param name="type"></param>
        /// <param name="quantity"></param>
        /// <param name="cost"></param>
        /// <param name="ubication"></param>
        public Product(Classification type, int quantity, double cost, string ubication)
        {
            this.Classification = type;
            this.Quantity = quantity;
            this.Cost = cost;
            this.Ubication = ubication;
        }

    }
}