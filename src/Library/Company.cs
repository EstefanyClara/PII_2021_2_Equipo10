using System;
using System.Collections;

namespace Proyect
{
    /// <summary>
    /// Esta clase representa la compania, 
    /// </summary>
    public class Company: User
    {
        public string name; 
        public string ubication; 
        public string rubro; 
        public ArrayList offers; 

        public Company(string name, string ubication, string rubro) : base (name,ubication,rubro)
        {
            this.Name = name; 
            this.Ubication = ubication; 
            this.Rubro = rubro; 
        }
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
        public string Rubro
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

        public ArrayList Offers
        {
            get
            {
                return this.offers;
            }
            set
            {
                this.offers = value;
            }
        }
        public  void PublicOffer()
        {
            
        }
    }
}
