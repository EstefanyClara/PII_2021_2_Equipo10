using System.Collections;

namespace ClassLibrary
{
    /// <summary>
    /// Representa el producto de una oferta
    /// </summary>
    public class Classification
    {
        private string validClassification;

/// <summary>
/// Determina la ubicacion de un producto.
/// </summary>
/// <value></value>
        public string ValidClassifications
        {
            get
            {
                return this.validClassification;
            }
            set
            {
                this.validClassification = value;
            }
        }
/// <summary>
/// Constructor de instancias de product.
/// </summary>
/// <param name="classification"></param>

        public Classification(string classification)
        {
            this.ValidClassifications= classification;
        }
    }
}