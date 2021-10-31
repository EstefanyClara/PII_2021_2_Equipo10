namespace   Proyect
{
    /// <summary>
    /// Clase que representa el rubro de una compania o emprendedor
    /// </summary>
    public class Rubro
    {
        private string rubroName;

/// <summary>
/// Propierti delnombre del rubro
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
/// Constructor de instancias de rubro
/// </summary>
/// <param name="rubroName"></param>

        public Rubro(string rubroName)
        {
            this.RubroName = rubroName;
        }
    }
}