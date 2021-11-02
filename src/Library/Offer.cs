using System.Collections.Generic;
using System.Collections;
using System;

namespace Proyect
{
    /// <summary>
    /// Representa la oferta de una compania.
    /// </summary>
    public class Offer
    {
        private bool ifConstant;

        private ProductOffer product;

        private List<Qualifications> qualifications;

        private ArrayList keyWords;

        private Emprendedor buyer;

        private DateTime timeAccepted;

/// <summary>
/// Constructor de offer, el mismo, crea una instancia del producto.
/// </summary>
/// <param name="ifConstant">Si el producto es recurrente.</param>
/// <param name="tipo">Tipo de procucto.</param>
/// <param name="quantity">Cantidad del producto.</param>
/// <param name="cost">Precio del producto.</param>
/// <param name="ubication">Ubicacion del producto.</param>
/// <param name="qualifications">Habilitaciones del producto.</param>
/// <param name="keyWords">Palabras claves del producto.</param>
        public Offer(bool ifConstant, Classification tipo, double quantity, double cost, string ubication, List<Qualifications> qualifications, ArrayList keyWords)
        {
            this.product = new ProductOffer(tipo, quantity, cost, ubication);
            this.Constant = ifConstant;
            this.Qualifications = qualifications;
            this.KeyWords = keyWords;
        }

        /// <summary>
        /// Obtiene un valor que indica si la oferta es constante o no.
        /// </summary>
        /// <value><c>true</c> si la oferta es constante, <c>false</c> en caso contrario.</value>
        public bool Constant
        {
            get
            {
                return this.ifConstant;
            }
            set
            {
                this.ifConstant = value;
            }
        }

        /// <summary>
        /// Obtiene el el producto de la oferta.
        /// </summary>
        /// <value></value>
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
        /// Obtiene las cualificaciones/habilitaciones necesarias para aceptar la oferta, esto lo establece cada compania.
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
        /// Obtiene el conjunto de palabras clave que se utilizan a la hora de buscar la oferta.
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
        /// Obtiene el compardor de la oferta, quien la acepto luego de que la haya buscado.
        /// </summary>
        /// <value><c>Emprendedor</c> si la oferta ya fue aceptada, lo que te dice tambien quien la acepto, <c>null</c> si no fue aceptada, por lo que se puede aceptar.</value>
        public Emprendedor Buyer
        {
            get
            {
                return this.buyer;
            }
            set
            {
                this.buyer = value;
            }
        }

        /// <summary>
        /// Propiedad para obtener la fecha y hora de cuando la oferta fue aceptada por un emprendedor.
        /// </summary>
        /// <value></value>
        public DateTime TimeAccepted
        {
            get
            {
                return this.timeAccepted;
            }

            set
            {
                this.timeAccepted = value;
            }
        }
    }
}