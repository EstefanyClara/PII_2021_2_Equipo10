using System;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;

namespace Proyect
{
    /// <summary>
    /// Esta clase administrador invita a los usuarios a registrarse.
    /// Clase singleton, solo una instancia de administrador.
    /// Esta clase contiene ademas, la lista de codigos para que una company se pueda registrar.
    /// Tambien contiene la lista de id de usuarios, que tienen poderio de administrador.
    /// Serializamos solo la lista de codigos, para asi mantener el encapsulamiento, ya que administrador solo deberia tener un constructor privado.
    /// </summary>
    public sealed class Administrator : IJsonConvertible
    {

        private readonly static Administrator _instance = new Administrator();
        private IList<string> tokens;

        private IList<string> admin_Id;

        private Administrator()
        {
            tokens = new List<string>() { "1234" };
            admin_Id = new List<string>();
        }

        /// <summary>
        /// Obtiene la instancia de administrador.
        /// </summary>
        /// <value>La instacia unica de administrador.</value>
        public static Administrator Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Obtiene la lista de tokes que el administrador crea, y que se usan para registrar companias.
        /// </summary>
        /// <value>La lista de codigos.</value>
        public IList<string> Tokens
        {
            get
            {
                return this.tokens;
            }
            set
            {
                this.tokens = value;
            }
        }

        /// <summary>
        /// Obteien la lista de id de aquellos que tienen el rol de administrador.
        /// </summary>
        /// <value>La lista de id de administradores.</value>
        public IList<string> Admin_Id
        {
            get
            {
                return this.admin_Id;
            }
        }
        /// <summary>
        /// Metodo que crea una compania si la misma ingreso un token correcto (Utiliza Creator).
        /// Tiene esta responsabilidad, ya que es la tiene la lista de tokes para validad (Por expert).
        /// </summary>
        /// <param name="companyToken">El codigo de regsitro.</param>
        /// <param name="name">El nombre de la compania.</param>
        /// <param name="ubication">La ubicacion de la compania.</param>
        /// <param name="rubro">El rubor de la compania.</param>
        /// <param name="user_Id">El id de usuario.</param>
        /// <param name="user_Contact">El mail de contacto de la compania.</param>
        /// <returns>La compania que si posee el codigo correcto, null en caso contrario.</returns>
        public Company ConfirmCompanyRegistration(string companyToken, string user_Id, string name, string ubication, Rubro rubro, string user_Contact)
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
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Genera un codigo con el cual una compania se podra registrar (Por expert).
        /// </summary>
        /// <returns>Codigo con el cual una compania se podra registrar.</returns>
        public string Invite()
        {
            List<string> tokenCharacters = new List<string>() { "$", "&", "#", "!", "?", "¿", "=", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random selector = new Random();
            string code = "";
            for (int position = 0; position <= 8; position++)
            {
                code = code + tokenCharacters[selector.Next(0, 16)];
            }
            this.Tokens.Add(code);
            return code;
        }

        /// <summary>
        /// Coloca el id de un usuario al la lista de id con rol de administrador (Por expert).
        /// </summary>
        /// <param name="confirmCode">El codigo de confirmacion.</param>
        /// <param name="user_Id">El id de usuario.</param>
        public bool AddAdministrator(string confirmCode, string user_Id)
        {
            if (confirmCode == "145879")
            {
                this.Admin_Id.Add(user_Id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Procesa si el id de usuario ingresado, es un id de un administrador (Por expert).
        /// </summary>
        /// <param name="user_Id">El id de usuario.</param>
        /// <returns>True si es administrador, false en caso contrario.</returns>
        public bool IsAdministrator(string user_Id)
        {
            if (this.Admin_Id.Contains(user_Id))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Convierte a json la lista de codigos para que una compania se pueda registrar (Por expert).
        /// </summary>
        /// <returns>La lista de tokens serializada.</returns>
        public string ConvertToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            return System.Text.Json.JsonSerializer.Serialize(this.Tokens, options);
        }

        /// <summary>
        /// Deserializa la lista de codigos (Por expert).
        /// </summary>
        public void Deserialize()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Preserve,
                WriteIndented = true
            };
            string json = System.IO.File.ReadAllText(@"../Library/Persistencia/CompanyCodes.json");
            this.Tokens = JsonConvert.DeserializeObject<IList<string>>(json);
        }
    }
}