using System;

namespace Proyect
{
    /// <summary>
    /// Superclase entity
    /// </summary>
    public class User
    {
/// <summary>
/// Nombre del usuario,sea una compania o emprendedor
/// </summary>
        protected string name;
/// <summary>
/// Ubicacion del usiario
/// </summary>
        protected string ubication;
/// <summary>
/// El rubro al que pertnece el usuario
/// </summary>
        protected Rubro rubro;
/// <summary>
/// Constructor de entity
/// </summary>
/// <param name="name"></param>
/// <param name="ubication"></param>
/// <param name="rubro"></param>
        public User(string name, string ubication, Rubro rubro)
        {
            this.Name = name;
            this.Ubication = ubication;
            this.Rubro = rubro;
        }
/// <summary>
/// Propiedad get y set del atributo del nombre
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
/// Propiedad get y set del atributo de la ubicacion
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
/// Propiedad get y set del atributo del rubro
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