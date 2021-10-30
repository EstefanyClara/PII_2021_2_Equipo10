using System;
using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Clase emprendedor.
    /// </summary>
    public class Emprendedor : User
    {
        private string qualifications;

        private string specializations;

        private List<Offer> purchasedOffer;
/// <summary>
/// Constructor de emprendedor
/// </summary>
/// <param name="name"></param>
/// <param name="ubication"></param>
/// <param name="rubro"></param>
/// <param name="qualifications"></param>
/// <param name="specializations"></param>
/// <returns></returns>
        public Emprendedor(string name, string ubication, string rubro, string qualifications, string specializations):base(name,ubication,rubro)
        {
            this.Qualifications = qualifications;
            this.Specializations = specializations;
        }
/// <summary>
/// Propiedad get y set de las habilitaciones
/// </summary>
/// <value></value>
        public string Qualifications
        {
            get
            {
                return this.qualifications;
            }
            set
            {
                this.qualifications = value;
            }
        }
/// <summary>
/// Propiedad Specializations
/// </summary>
/// <value></value>
        public string Specializations
        {
            get
            {
                return this.specializations;
            }
            set
            {
                this.specializations = value;
            }
        }
    }
}