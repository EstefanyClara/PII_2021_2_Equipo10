using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proyect
{
    /// <summary>
    /// Clase que representa el rubro de una compania o emprendedor.
    /// </summary>
    public class Rubro
    {
        private string rubroName;

        /// <summary>
        /// Property del nombre del rubro.
        /// </summary>
        /// <value>El nombre del rubro.</value>
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