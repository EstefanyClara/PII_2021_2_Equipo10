using System.Collections;

namespace Proyect
{
    /// <summary>
    /// Representa la oferta de una compania
    /// </summary>
    public class Offer
    {
        private bool ifConstant;

        private ProductOffer product;

        private string qualifications;

        private ArrayList keyWords;

        private Emprendedor buyer;

/// <summary>
/// Constructor de offer, el mismo, crea una instancia del producto
/// </summary>
/// <param name="ifConstant"></param>
/// <param name="tipo"></param>
/// <param name="quantity"></param>
/// <param name="cost"></param>
/// <param name="ubication"></param>
/// <param name="qualifications"></param>
/// <param name="keyWords"></param>
        public Offer(bool ifConstant, Classification tipo, int quantity, double cost, string ubication, string qualifications, string keyWords)
        {
            this.product = new ProductOffer(tipo,quantity,cost,ubication);
            this.Constant = ifConstant;
            this.Qualifications = qualifications;

            this.KeyWords = new ArrayList();

            string[]  words = keyWords.Split(" ");
            foreach( string word in words)
            {
                this.KeyWords.Add(word);
            }
            
        }

/// <summary>
/// Obtiene un valor que indica si la oferta es constante o no
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
/// Obteien las cualificaciones/habilitaciones neceraias para aceptar la oferta, esto lo establce cada compania
/// </summary>
/// <value></value>

        public string Qualifications
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
/// ´Obtiene el conjunto de palabras clave que se utilizan a la hora de buscar la oferta
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
/// Obtiee el compardor de la oferta, quien la acepto luego de que la haya buscado
/// </summary>
/// <value><c>Emprendedor</c> si la oferta ya fue aceptada, lo que te dice tabien quien la acepto, <c>null</c> si no fue aceptada, por lo que se puede aceptar.</value>

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
    }
}