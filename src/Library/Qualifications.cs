namespace   Proyect
{
    /// <summary>
    /// Clase que representa las habilitaciones de una oferta o emprendedor.
    /// </summary>
    public class Qualifications
    {
        private string qualificationName;

        /// <summary>
        /// Propiedad del nombre de la habilitacion.
        /// </summary>
        /// <value></value>
        public string QualificationName
        {
            get
            {
                return this.qualificationName;
            }
            set
            {
                this.qualificationName = value;
            }
        }
        
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Qualifications"/>.
        /// </summary>
        /// <param name="qualificationName">Nombre de las habilitaciones.</param>
        public Qualifications(string qualificationName)
        {
            this.QualificationName = qualificationName;
        }
    }
}
