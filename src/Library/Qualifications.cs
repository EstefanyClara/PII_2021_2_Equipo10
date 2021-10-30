namespace   Proyect
{
    /// <summary>
    /// Clase que representa las habilitaciones de una oferta o emprendedor
    /// </summary>
    public class Qualifications
    {
        private string qualificationName;

/// <summary>
/// Propierti delnombre de la habilitacion
/// </summary>
/// <value></value>
        public string Qualification
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
/// Constructor de instancias de qualification
/// </summary>
/// <param name="qualificationName"></param>

        public Qualifications(string qualificationName)
        {
            this.Qualification = qualificationName;
        }
    }
}