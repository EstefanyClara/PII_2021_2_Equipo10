namespace   Proyect
{
    /// <summary>
    /// Clase que representa el rubro de una compania o emprendedor.
    /// </summary>
    public class Rubro
    {
        private string rubroName;

        /// <summary>
        /// Propierty del nombre del rubro.
        /// </summary>
        /// <value></value>
        public string RubroName
        {
            get
            {
                return this.rubroName;
            }
            set
            {
                this.rubroName = value;
            }
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Rubro"/>
        /// </summary>
        /// <param name="rubroName">Nombre del rubro.</param>
        public Rubro(string rubroName)
        {
            this.RubroName = rubroName;
        }
    }
}