using System;

namespace Proyect
{
    /// <summary>
    /// Es la clasificacion que va a tener cada producto
    /// </summary>
    public class Classification
    {
        private string category;

        /// <summary>
        /// Devuelve el nombre de la clasificacion
        /// </summary>
        /// <value></value>
        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        /// <summary>
        /// Constructor de las clasificaciones
        /// </summary>
        /// <param name="category"></param>
        public Classification(string category)
        {
            this.Category = category;
        }
    }
}