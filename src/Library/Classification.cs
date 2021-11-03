namespace Proyect
{
    /// <summary>
    /// Esta clase representa una clasificacion para un producto.
    /// </summary>
    public class Classification
    {
        private string category;

        /// <summary>
        /// Obtiene el valor de una clasificacion para un Producto.
        /// </summary>
        /// <value>this.category</value>
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
        /// Inicializa una nueva instancia de la clase <see cref="Classification"/>.
        /// </summary>
        /// <param name="category">Categoria de clasificación.</param>
        public Classification(string category)
        {
            this.Category = category;
        }
    }
}

