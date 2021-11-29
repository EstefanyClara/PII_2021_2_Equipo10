using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


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
        /// Id del usuario, unico por cada uno.
        /// </summary>
        protected string user_Id;

        /// <summary>
        /// Contacto del usurario.
        /// </summary>
        protected string user_Contact;

        protected List<ConstantOffer> ofertasConstantes = new List<ConstantOffer>();
        protected List<NonConstantOffer> ofertasNoConstantes = new List<NonConstantOffer>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="User"/>
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="ubication">Ubicacion del usuario.</param>
        /// <param name="rubro">Rubro del usuario.</param>
        /// <param name="user_Id">Rubro del usuario.</param>
        /// <param name="user_Contact">Rubro del usuario.</param>
        public User(string user_Id, string name, string ubication, Rubro rubro, string user_Contact)
        {
            if (name == "" || ubication == "")
            {
                throw new EmptyUserBuilderException("Los datos ingresados no son validos, nombre o ubicacion vacios");
            }
            this.Name = name;
            this.Ubication = ubication;
            this.Rubro = rubro;
            this.user_Id = user_Id;
            this.User_Contact = user_Contact;
        }

        [JsonConstructor]
        public User()
        {

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

        /// <summary>
        /// Obtiene el id del usuario.
        /// </summary>
        /// <value></value>
        public string User_Id
        {
            get
            {
                return this.user_Id;
            }
        }

        /// <summary>
        /// Obtiene el contacto del usuraio.
        /// </summary>
        /// <value></value>
        public string User_Contact
        {
            get
            {
                return this.user_Contact;
            }
            set 
            {
                this.user_Contact = value;
            }
        }

        [JsonInclude]
        public List<ConstantOffer> OfertasConstantes
        {
            get{ return this.ofertasConstantes;}
            set{this.ofertasConstantes = value;}
        }

        [JsonInclude]
        public List<NonConstantOffer> OfertasNoConstantes
        {
            get{ return this.ofertasNoConstantes;}
            set{this.ofertasNoConstantes = value;}
        }
    }
}