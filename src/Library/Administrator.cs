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

        private List<string> admin_Id;

        private Administrator()
        {
            tokens = new List<string>(){"1234"};
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
        /// Obteien la lista de id de aquellos que tienen el rol de administrador.
        /// </summary>
        /// <value></value>
        public List<string> Admin_Id
        {
            get
            {
                return this.Tokens;
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
        public Company ConfirmCompanyRegistration(string companyToken,string user_Id, string name, string ubication, Rubro rubro, string user_Contact)
        {   
            if (tokens.Contains(companyToken))
            {
                try
                {
                    this.Tokens.Remove(companyToken);
                    return new Company(user_Id, name, ubication, rubro, user_Contact);
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

        /// <summary>
        /// Genera un codigo con el cual una compnai se podra registrar.
        /// </summary>
        /// <returns>Codigo con el cual una compania se podra registrar.</returns>
        public string Invite()
        {
            List<string> tokenCharacters = new List<string>(){"$","&","#","!","?","¿","=","1","2","3","4","5","6","7","8","9"};
            Random selector = new Random();
            string code = "";
            for(int position = 0; position<=8; position++)
            {
                code = code + tokenCharacters[selector.Next(0,17)];
            }
            this.Tokens.Add(code);
            return code;
        }

        /// <summary>
        /// Coloca el id de un usuario al la lista de id con rol de administrador.
        /// </summary>
        /// <param name="confirmCode"></param>
        /// <param name="user_Id"></param>
        public bool AddAdministrator(string confirmCode, string user_Id)
        {
            if (confirmCode == "145879")
            {
                this.Admin_Id.Add(user_Id);
                return true;
            }
            return false;
        }
    }
}