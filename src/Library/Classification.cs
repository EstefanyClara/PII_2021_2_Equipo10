using System.Collections;

namespace   Proyect
{
    /// <summary>
    /// Representa el producto de una oferta
    /// </summary>
    public class Classification
    {
        private string validClassification;

/// <summary>
/// Propiedad de validClasification, el cual guarda el nombre del tipo del product,es decir, su clasificacion
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
/// Constructor de instancias de clasification.
/// </summary>
/// <param name="classification"></param>

        public Classification(string classification)
        {
            this.ValidClassifications= classification;
        }
    }
}