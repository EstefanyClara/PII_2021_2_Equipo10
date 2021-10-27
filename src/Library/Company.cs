using System; 

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
        public Array offers; 

        public Company(string name, string ubication, string rubro)
        {
            this.Name=name; 
            this.Ubication= ubication; 
            this.Rubro= rubro; 
        }
        public string Name
        {
            get
            {
                return this.name; 
            }   
            set
            {
                this.name=value; 
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
                this.ubication=value; 
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
                this.rubro=value; 
            } 
        }
        public  void PublicOffer()
        {
            
        }
    }
}
