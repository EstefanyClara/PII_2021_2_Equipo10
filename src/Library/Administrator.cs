using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect
{
    /// <summary>
    /// Esta clase administrador invita a los usuarios a registarse. 
    /// </summary>
    public class Administrator
    {
       
        /// <summary>
        /// Este metodo invita a una compania y la registra.
        /// </summary>
        public void invite()
        {   
            Console.WriteLine("Bienvenido!! Este es un chatbot si quiere registrarse ingrese R y si no ignore este mensaje."); 
            string response= Console.ReadLine().ToUpper(); 
            if (response== "R")
            {
                Console.WriteLine("Usted a aceptado la invitación.");
                Console.WriteLine("Ingrese el nombre de su compañia:");
                string name= Console.ReadLine(); 
                Console.WriteLine("Ingrese su ubicación:");
                string ubication= Console.ReadLine();
                Console.WriteLine("Ingrese el rubro de su compañia:");
                string nameRubro=Console.ReadLine();
                Rubro rubro= new Rubro(nameRubro);
                Company company=new Company(name, ubication, rubro);
            }
            else
            {
                Console.WriteLine("Usted a rechazado la invitación."); 
            }
        }
    }
}