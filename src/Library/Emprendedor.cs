using System;
using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Clase emprendedor.
    /// </summary>
    public class Emprendedor : Entity
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
/// Propiedad get y set
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
/// <summary>
/// Metodo para buscar por palabras clave
/// </summary>
/// <param name="words"></param>
        public void SearchByKeywords(string words)
        {
            string[] keywords = words.Split(" ");

        }
/// <summary>
/// Metodo para buscar por ubicacion
/// </summary>
/// <param name="ubication"></param>
        public void SearchByUbications(string ubication)
        {

        }
/// <summary>
/// Metodo para buscar por tipo
/// </summary>
/// <param name="tipo"></param>
        public void SearchByType(string tipo)
        {
            
        }

        private void AceptOffer(Offer oferta)
        {
            this.purchasedOffer.Add(oferta);
            oferta.Buyer = this;
        }
    }
}