namespace Proyect
{
    /// <summary>
    /// Superclase user de Emprendedor y Company (quienes estan en una relaciontaxonomica con esta clase).
    /// </summary>
    public class User
    {
        /// <summary>
        /// Nombre del usuario,sea una compania o emprendedor.
        /// </summary>
        protected string name;
        /// <summary>
        /// Ubicacion del usiario
        /// </summary>
        protected string ubication;
        /// <summary>
        /// El rubro al que pertnece el usuario.
        /// </summary>
        protected Rubro rubro;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="User"/>
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="ubication">Ubicacion del usuario.</param>
        /// <param name="rubro">Rubro del usuario.</param>
        public User(string name, string ubication, Rubro rubro)
        {
            if (name == "" || ubication == "")
            {
                throw new EmptyUserBuilderException("Los datos ingresados no son validos, nombre o ubicacion vacios");
            }
            this.Name = name;
            this.Ubication = ubication;
            this.Rubro = rubro;
        }
        /// <summary>
        /// Propiedad get y set del atributo del nombre.
        /// </summary>
        /// <value></value>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        /// <summary>
        /// Propiedad get y set del atributo de la ubicacion.
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
        /// Propiedad get y set del atributo del rubro.
        /// </summary>
        /// <value></value>
        public Rubro Rubro
        {
            get
            {
                return this.rubro;
            }
            set
            {
                this.rubro = value;
            }
        }
    }
}