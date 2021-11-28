using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;
using System.Globalization;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class RegisterHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegisterHandler"/>. Esta clase procesa el mensaje "/Registrar" de un usuario.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/registrar"};
        }

        /// <summary>
        /// Procesa el mensaje "/registrar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {

            if (message.Text.ToLower().Replace(" ","").Equals("/registrar"))
            {
                if (AppLogic.Instance.GetEmprendedor(message.Id) != null | AppLogic.Instance.GetCompany(message.Id) != null)
                {
                    response = "Usted ya se encuentra registrado";
                    return true;
                }
                if(!DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    response = "Bienvenido a C4BOT\n\n¿Posee un Token?\nIngrese /si si tiene uno y asi registrarse como empresa o /no si no lo tiene y registrarse como emprendedor";
                    List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
                    DataUserContainer.Instance.UserDataHistory.Add(message.Id,lista);
                    DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/registrar");
                    return true;
                }else
                {
                    response = "Usted ya esta en proceso de registro";
                    return true;
                }
            }if (DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id) && DataUserContainer.Instance.UserDataHistory[message.Id][0][0] == "/registrar")
            {
                if (message.Text.ToLower().Replace(" ","").Equals("/si") & DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0)
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/si");
                    response = "Usted se registrara como empresa en esta aplicacion\nIngrese el codigo: ";
                    return true;
                }if (message.Text.ToLower().Replace(" ","").Equals("/no") & DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0)
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/no");
                    response = "Usted se registrara como usuario en esta aplicacion\n¿Esta de acuerdo? (/si o /no)";
                    return true;
                }if (DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0)
                {
                    response = "Debe ingresar /si o /no";
                    return true;
                }
                string position = DataUserContainer.Instance.UserDataHistory[message.Id][1][0];
                List<string> userData = DataUserContainer.Instance.UserDataHistory[message.Id][1];
                switch(userData.Count)
                {
                    case 1:
                    if(position.Equals("/si"))
                    {
                        if (!message.Text.Contains("?"))
                        {
                        userData.Add(message.Text); //El token de la compania
                        response = "Ingrese el nombre de su compania";
                        }else
                        {
                            response = "Por favor ingrese un dato valido";
                        }
                    }else
                    {
                        if(message.Text.ToLower().Replace(" ","").Equals("/si"))
                        {
                            userData.Add("Emprendedor no necesita Token (este es u espacio momentaneo presente hasta que el emprendedor ingrese sus habilitaciones)");
                            response = "Ingrese el nombre de su emprendimiento";
                            return true;
                        }if(message.Text.ToLower().Replace(" ","").Equals("/no"))
                        {
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                            response = "Recuerde que puede registraser como usuario en cualquier momento.\n\nPara registrarse como compania contacto con el administrador";
                            return true;
                        }
                        response = "Dato erroneo ingrese /si o /no";
                        return true;
                    }
                    return true;
                    case 2:
                        if (!message.Text.Contains("?"))
                        {
                            userData.Add(message.Text); //El nombre
                            response = "Ingrese su ubicacion actual";
                        }else
                        {
                            response = "Por favor ingrese un dato valido";
                        }
                        return true;
                    case 3:
                        if (!message.Text.Contains(",") & !message.Text.Contains("."))
                        {
                            userData.Add(message.Text); //La ubicacion de la compania
                        }else
                        {
                            response = "Por favor ingrese un dato valido";
                            return true;
                        }
                        StringBuilder mensaje = new StringBuilder();
                        mensaje.Append("A continuacion ingrese el rubro al cual pertenece.\n\nEliga entre los habilitados de la aplicacion indicando su indice.\n");
                        List<Rubro> validRubros = AppLogic.Instance.Rubros;
                        int indice = 0;
                        foreach(Rubro item in validRubros)
                        {
                            indice++;
                            mensaje.Append($"{indice}-{item.RubroName}\n");
                        }
                        response = mensaje.ToString();
                        return true;
                    case 4:
                        int number;
                        if (int.TryParse(message.Text, out number))
                        {
                            if (AppLogic.Instance.Rubros.Count - number >= 0)
                            {
                                number--;
                                userData.Add(number.ToString());
                                response = "Ahora ingrese su mail de contacto.";
                                return true;
                            }else
                            {
                                response = "Numero invalido";
                                return true;
                            }
                        }else
                        {
                            response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir el primer rubro)";
                            return true;
                        }
                    case 5:
                        if (message.Text.Contains("@") && message.Text.Contains("."))
                        {
                            userData.Add(message.Text.Trim(' '));
                            if (userData[0].Equals("/si"))
                            {
                                response = $"Por favor, veo si los datos ingresados son correctos.\nCodigo de registro: {userData[1]}\nNombre de la compania: {userData[2]}\nUbicacíon de la companiai: {userData[3]}\nRubro de la compania: {AppLogic.Instance.Rubros[Convert.ToInt32(userData[4])].RubroName}\nMail de contacto: {userData[5]}\n\nSi son correctos ingrese '/si', en caso contrario '/no'";
                            }else
                            {
                                mensaje = new StringBuilder();
                                mensaje.Append("A continuacion ingrese las habilitaciones que posee.\n\nEliga entre los habilitadoas por la aplicacion indicando su indice (puede elegir mas de una).\n");
                                int index = 0;
                                foreach (Qualifications item in AppLogic.Instance.Qualifications)
                                {
                                    index++;
                                    mensaje.Append($"{index}-{item.QualificationName}\n");
                                }
                                response = mensaje.ToString();
                            }
                        }else
                        {
                            response = "Debe ingresar un formato valido para el mail.";
                        }
                        return true;
                    case 6:
                        if ( userData[0].Equals("/si"))
                        {
                            if (message.Text.ToLower().Replace(" ","").Equals("/si"))
                            {
                                if(AppLogic.Instance.RegistrarCompany(userData[1], message.Id, userData[2], userData[3], AppLogic.Instance.Rubros[Convert.ToInt32(userData[4])], userData[5]))
                                {
                                    DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                    response = "Usted se a registrado con exito";
                                    return true;
                                }else
                                {
                                    DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                    response = "ERROR AL REGISTRAR\n\nRevise si el codigo ingresado es correcto o si ingreso algun dato erroneo";
                                    return true;
                                }
                            }if(message.Text.ToLower().Replace(" ","").Equals("/no"))
                            {
                                DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                response = "Se procedera a eliminar todos los datos ingresados.\n\nEn el caso de querer volver a registrarse, por favor use el comando '/Registrar'.";
                                return true;
                            }
                            response = "Dato incorrecto\n Por favor ingrese '/si' o '/no'.";
                            return true;
                        }else
                        {
                            if(!message.Text.ToLower().Replace(" ","").Equals("/stop"))
                            {
                                if (userData[1] == "Emprendedor no necesita Token (este es u espacio momentaneo presente hasta que el emprendedor ingrese sus habilitaciones)")
                                {
                                    userData.RemoveAt(1);
                                    userData.Add("");
                                }
                                if (int.TryParse(message.Text, out number))
                                {
                                    if (AppLogic.Instance.Qualifications.Count - number >= 0 )
                                    {
                                        if (!userData[5].Contains(message.Text.Replace(" ","")))
                                        {
                                            string habilitacion = userData[5];
                                            habilitacion = habilitacion + "-" + message.Text.Replace(" ","");
                                            userData.RemoveAt(5);
                                            userData.Add(habilitacion);
                                            response = "Se ha agregado la habilitacion";
                                            return true;
                                        }else
                                        {
                                            response = "La habilitacion indicada ya se encuentra selecionada";
                                            return true;
                                        }
                                    }else
                                    {  
                                        response = "El indice ingresado no es valido";
                                        return true;
                                    }
                                }else
                                {
                                    response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir la primera habilitacion)";
                                    return true;
                                }
                            }
                        }
                        response = "Ahora ingrese su especializacion:";
                        userData.Add("");
                        return true;
                    case 7:
                        if (!message.Text.Contains("?"))
                        {
                            userData.RemoveAt(6);
                            userData.Add(message.Text);
                        }else
                        {
                            response = "Por favor ingrese un dato valido";
                            return true;
                        }
                        mensaje = new StringBuilder();
                        string[] habilitaciones = userData[5].Split("-");
                        foreach(string item in habilitaciones)
                        {
                            if (int.TryParse(item, out number))
                            {
                                mensaje.Append($"\n-{AppLogic.Instance.Qualifications[number-1].QualificationName}");
                            }
                        }
                        userData.Add(" ");
                        response = $"Por favor, veo si los datos ingresados son correctos.\nNombre: {userData[1]}\nUbicacion: {userData[2]}\nRubro: {AppLogic.Instance.Rubros[Convert.ToInt32(userData[3])].RubroName}\nMail de contacto: {userData[4]}\nHabilitaciones: {mensaje}\nEspecializacion: {userData[6]}\n\nSi son correctos ingrese '/si', en caso contrario '/no'";
                        return true;
                    case 8:
                        if (message.Text.ToLower().Replace(" ","").Equals("/si"))
                        {
                            string[] habilitacionesLista = userData[5].Split("-");
                            List<Qualifications> lista = new List<Qualifications>();
                            foreach(string item in habilitacionesLista)
                            {
                                if (int.TryParse(item, out number))
                                {
                                    lista.Add(AppLogic.Instance.Qualifications[number-1]);
                                }
                            }
                            
                            AppLogic.Instance.RegisterEntrepreneurs(message.Id, userData[1], userData[2], AppLogic.Instance.Rubros[Convert.ToInt32(userData[3])], lista, userData[6], userData[4]);
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                            response = "Usted se a registrado con exito";
                            return true;
                        }if(message.Text.ToLower().Replace(" ","").Equals("/no"))
                        {
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                            response = "Se procedera a eliminar todos los datos ingresados.\n\nEn el caso de querer volver a registrarse, por favor use el comando '/Registrar'.";
                            return true;
                        }
                        response = "Dato incorrecto\n Por favor ingrese '/si' o '/no'.";
                    return true;
                }
                
            }
            response = string.Empty;
            return false;
        }
    }
}