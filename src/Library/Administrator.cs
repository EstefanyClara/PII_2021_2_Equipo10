using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect
{
    /// <summary>
    /// Esta clase administrador invita a los usuarios a registarse.
    /// Clase singleton, solo una instancia de administrador. 
    /// </summary>
    public sealed class Administrator
    {

        private readonly static Administrator _instance = new Administrator();
        private List<string> tokens;

        private Administrator()
        {
            tokens = new List<string>(){"1234"};
            PrecargarDatos();
        }

        /// <summary>
        /// Obtiene la instancia de administrador.
        /// </summary>
        /// <value></value>
        public static Administrator Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Obteien la lista de tokes que el administardo coloca.
        /// </summary>
        /// <value></value>
        public List<string> Tokens
        {
            get
            {
                return this.tokens;
            }
        }
        /// <summary>
        /// Metodo que crea una compania si la misma ingreso un token correcto (Utiliza Creator).
        /// </summary>
        /// <param name="companyToken"></param>
        /// <param name="name"></param>
        /// <param name="ubication"></param>
        /// <param name="rubro"></param>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        public Company Invite(string companyToken,string user_Id, string name, string ubication, Rubro rubro)
        {   
            if (tokens.Contains(companyToken))
            {
                try
                {
                    return new Company(user_Id, name, ubication, rubro);
                }
                catch (EmptyUserBuilderException e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }else 
            {
                return null;
            }
        }
        private void PrecargarDatos()
        {
            tokens.Add("Pepe");
            tokens.Add("Pepesito");
        }
    }
}