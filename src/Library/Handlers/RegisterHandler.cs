using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class RegisterHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegisterHandler"/>. Esta clase procesa el mensaje "/Registrar" de un usuario y todos los datos necerarios para registralo.
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

            if (this.Keywords.Contains(message.Text.ToLower().Replace(" ",""))) //Pregunto si el mensaje del usurio corresponde a los que el handler maneja.
            {
                if (AppLogic.Instance.GetEmprendedor(message.Id) != null | AppLogic.Instance.GetCompany(message.Id) != null) //Pregunto si el usuraio ya esta registrado, verificando de que se no encuentre como compania o emprendedor.
                {
                    response = "Usted ya se encuentra registrado"; //Mensaje que indica que ya esta registrado.
                    return true;
                }
                if(!DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id)) //Pregunto si el usuraio se encuentra en el diccionario que maneja la informacion y las posciones de cada usurario.
                {
                    //Si el usuario no se encuentra, significa que no estaba en nigun estado en especifico, por lo que su mensje es valido.
                    response = "Bienvenido a C4BOT\n\n¿Posee un Token?\nIngrese /si si tiene uno y asi registrarse como empresa o /no si no lo tiene y registrarse como emprendedor";
                    List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
                    DataUserContainer.Instance.UserDataHistory.Add(message.Id,lista); 
                    DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/registrar"); //Agrego el usurio al diccionario que maneja la informacion de los chats de cada usuario, y agrego el mensaje que identifica en que handler esta.
                    return true;
                }else
                {
                    //Si el usuario se encuentra en el diccionario, significa que ya esta en algun proceso del hanlder.
                    response = "Usted ya esta en proceso de registro";
                    return true;
                }
            }if (DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id) && DataUserContainer.Instance.UserDataHistory[message.Id][0][0] == "/registrar") //Si el mensaje no es uno admitido por el hanlder, pregunto si el usurio esta en el diccionario que maneja la informacion dle chat, y si esta, pregunto si la palabra de identificacion del hanlder, es una admitida por este handler.
            {
                if (message.Text.ToLower().Replace(" ","").Equals("/si") & DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0) //Proceso si el mensaje esra para verificar que tipo de usuario es.
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/si");
                    response = "Usted se registrara como empresa en esta aplicacion\nIngrese el codigo: ";
                    return true;
                }if (message.Text.ToLower().Replace(" ","").Equals("/no") & DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0) //Proceso si el mensaje esra para verificar que tipo de usuario es.
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/no");
                    response = "Usted se registrara como usuario en esta aplicacion\n¿Esta de acuerdo? (/si o /no)";
                    return true;
                }if (DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0) //Me fijo en que instancia del hanlder esta, para saber si el mensaje que ingreso es valido.
                {
                    response = "Debe ingresar /si o /no";
                    return true;
                }
                string position = DataUserContainer.Instance.UserDataHistory[message.Id][1][0]; // Posicion de la lista que tiene el stirng que identifica si el usuario se esta registrando como emprendedor o empresa.
                List<string> userData = DataUserContainer.Instance.UserDataHistory[message.Id][1]; //La lista que almacena toda la informacion que ingresa el usuario.

                // Para saber en que posicion del handler esta el usuario, se pregunta la cantidad de items que tiene la lista de informacion que ingresa el usuario.
                
                switch(userData.Count)
                {
                    case 1: //Si el lista de informacion tiene un elemento, significa que el usuario ingreso el primer dato necesario para registrase.
                    if(position.Equals("/si")) //Verifico si el usuario se va a registrar como compania o emprendedor.
                    {
                        userData.Add(message.Text); //Si el usuario se va a registrar como compnai, significa que ingreso el token, por lo que agrego el mensaje ingresado a la lista de informacion.
                        response = "Ingrese el nombre de su compania"; //Envio el mensaje de que deberia ingresar el usuario en su proxima interracion.

                    }else
                    {
                        if(message.Text.ToLower().Replace(" ","").Equals("/si")) //Regulo si el mensaje que ingreso el usuraio es valido.
                        {
                            userData.Add("Emprendedor no necesita Token (este es u espacio momentaneo presente hasta que el emprendedor ingrese sus habilitaciones)"); //Coloco un dato hemifiero en el diccionario de infromacion, para cuando el usuario (emprendedor) ingrese otro dato, salte al siguiente case.
                            response = "Ingrese el nombre de su emprendimiento";
                            return true;
                        }if(message.Text.ToLower().Replace(" ","").Equals("/no")) //Regulo nuevamnete si el mensaje que ingreso el usuraio es valido.
                        {
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                            response = "Recuerde que puede registraser como usuario en cualquier momento.\n\nPara registrarse como compania contacto con el administrador";
                            return true;
                        }
                        response = "Dato erroneo ingrese /si o /no"; //Si el programa llego a este punto, significa que el usuario ingreso un dato diferente al esperado.
                        return true;
                    }
                    return true;
                    case 2: //La lista de informacion tiene dos elementos, por lo que el usuario habra ingresado su nombre.
                        if (!message.Text.Contains("?")) //Regulo si el mensaje tiene algun caracter no valido.
                        {
                            userData.Add(message.Text); //El nombre.
                            response = "Ingrese su ubicacion actual"; //Proximo tipo de informacion a ingresar.
                        }else
                        {
                            response = "Por favor ingrese un dato valido"; //Mensaje que se envia si el  mensaje ingresado no es valido.
                        }
                        return true;
                    case 3: //La lista de informacion tiene tres elementos, por lo que el usuario habra ingresado su ubicacíon.
                        if (!message.Text.Contains(",") & !message.Text.Contains(".")) // Regulo si el mensaje tiene caracteres no validos.
                        {
                            userData.Add(message.Text); //La ubicacion de la compania
                        }else
                        {
                            response = "Por favor ingrese un dato valido"; //Mensaje que se envia si el  mensaje ingresado no es valido.
                            return true;
                        }
                        StringBuilder mensaje = new StringBuilder(); //Constuyo el siguinete mensaje que se le ingresara al usuario.
                        mensaje.Append("A continuacion ingrese el rubro al cual pertenece.\n\nEliga entre los habilitados de la aplicacion indicando su indice.\n");
                        int indice = 0;
                        foreach(Rubro item in AppLogic.Instance.Rubros) //Itero la lista de rubros validos de la logica.
                        {
                            indice++;
                            mensaje.Append($"{indice}-{item.RubroName}\n");
                        }
                        response = mensaje.ToString();
                        return true;
                    case 4: //La lista de informacion tiene cuatro elementos, por lo que el usuario habra ingresado el indice del rubro al que pertenece.
                        int number;
                        if (int.TryParse(message.Text, out number)) //Pregunto si el mensaje que ingreso, que seria el indice del rubro al que pertenece, se puede transformar a int.
                        {
                            if (AppLogic.Instance.Rubros.Count - number >= 0) //Pregunto si el numero en si es valido, y pertenece a los indices mostrados.
                            {
                                number--; //Convierto el indice ingresado, al indice real de la lista de rubros.
                                userData.Add(number.ToString()); //Agrego el indice a la lista de informacion para que salte al siguiente case la proxima vez.
                                response = "Ahora ingrese su mail de contacto:"; //Le envio el mensaje con el proximo dato a ingresar.
                                return true;
                            }else
                            {
                                response = "Numero invalido"; //El numero ingresado es invalido.
                                return true;
                            }
                        }else
                        {
                            response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir el primer rubro)"; // el dato ingresado no es un numero.
                            return true;
                        }
                    case 5: //La lista de informacion tiene cinco elementos, por lo que el usuario habra ingresado su mail de contacto.
                        if (message.Text.Contains("@") && message.Text.Contains(".")) // Pregunto si el mensaje ingresado tiene los caracteres comuos a cualquier mail.
                        {
                            userData.Add(message.Text.Trim(' '));
                            if (userData[0].Equals("/si")) //Verifico si el usuario se esta registando como compania o emprendedor, ya que en esta instacia los datos de registro diferien entre ambos.
                            {
                                //Si el usuario se esta registrando como compania, envio un mensaje para que verifique si ingreso todos los datos bien.
                                response = $"Por favor, veo si los datos ingresados son correctos.\nCodigo de registro: {userData[1]}\nNombre de la compania: {userData[2]}\nUbicacíon de la companiai: {userData[3]}\nRubro de la compania: {AppLogic.Instance.Rubros[Convert.ToInt32(userData[4])].RubroName}\nMail de contacto: {userData[5]}\n\nSi son correctos ingrese '/si', en caso contrario '/no'";
                            }else
                            {
                                //Si el usuario se esta registrando como emprendedor, debera ingresar las habiliatciones que posee.
                                mensaje = new StringBuilder(); //Construyo el mensaje con las habilitaciones de la aplicacion.
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
                            response = "Debe ingresar un formato valido para el mail."; //Mensaje que se muestra si el usuario no ingreso un mensaje con los caracteres necesarios.
                        }
                        return true;
                    case 6: //La lista de informacion tiene cinco elementos, por lo que el usuario habra ingreado o el indice de la habiliatcion que tiene, o el mensaje de confirmacion de registro.
                        if ( userData[0].Equals("/si")) //Verifico nuevamente si el usuario se registra como compania o emprendedor.
                        {
                            if (message.Text.ToLower().Replace(" ","").Equals("/si")) //Si es compania, verifico si el mensaje que ingreso es valido, y si confimro su registro.
                            {
                                if(AppLogic.Instance.RegistrarCompany(userData[1], message.Id, userData[2], userData[3], AppLogic.Instance.Rubros[Convert.ToInt32(userData[4])], userData[5])) //Pregunto si el resgistro se dio correctamente, o si ingreso un token invalido.
                                {
                                    DataUserContainer.Instance.UserDataHistory.Remove(message.Id); //Como ya se registro, elimino el usuario del diccionario que almacena los usuarios que estan interactaundo con el bot.
                                    response = "Usted se a registrado con exito"; //Mensaje de aviso.
                                    return true;
                                }else
                                {
                                    DataUserContainer.Instance.UserDataHistory.Remove(message.Id); //Si el token ingresado es invalido, elimino el usuario del diccionario que almacena los usuarios que estan interactaundo con el bot, para que pueda volver a empezar.
                                    response = "ERROR AL REGISTRAR\n\nRevise si el codigo ingresado es correcto o si ingreso algun dato erroneo"; //Mnesaje de aviso.
                                    return true;
                                }
                            }if(message.Text.ToLower().Replace(" ","").Equals("/no")) //Verifico si el usuario quiere cancelar su registro.
                            {
                                DataUserContainer.Instance.UserDataHistory.Remove(message.Id); //Elimino al usuario del diccionario de que teien a todos los usuarios que estan interactuando con el bot.
                                response = "Se procedera a eliminar todos los datos ingresados.\n\nEn el caso de querer volver a registrarse, por favor use el comando '/Registrar'."; //Mensaje de aviso.
                                return true;
                            }
                            response = "Dato incorrecto\n Por favor ingrese '/si' o '/no'."; //Mensaje que se envia si el usuario no ingreso ninguno de los datos esperados.
                            return true;
                        }else
                        {
                            if(!message.Text.ToLower().Replace(" ","").Equals("/stop")) //Verifico si el usuario quiere dejar de agregar habilitaciones.
                            {
                                if (userData[1] == "Emprendedor no necesita Token (este es u espacio momentaneo presente hasta que el emprendedor ingrese sus habilitaciones)") //Si el usuario ingreso alguna habiliatcion, verifico si la lista de informacion aun tiene el dato hefimero.
                                {
                                    userData.RemoveAt(1); // Saco el dato hefimero para que se pueda remplazar por el indice de la habilitacion que ingreso.
                                    userData.Add("");
                                }
                                if (int.TryParse(message.Text, out number)) //Verifico si el dato ingresado es un numero.
                                {
                                    if (AppLogic.Instance.Qualifications.Count - number >= 0 ) //Verifico si el numero ingresado corresponde a los indices mostrados.
                                    {
                                        if (!userData[5].Contains(message.Text.Replace(" ",""))) //Verifico si el indice ya se encuntra en el string que tiene todos los indices.
                                        {
                                            string habilitacion = userData[5];
                                            habilitacion = habilitacion + "-" + message.Text.Replace(" ",""); //Agrego el nuevo indice con el delimitador "-".
                                            userData.RemoveAt(5);
                                            userData.Add(habilitacion);
                                            response = "Se ha agregado la habilitacion";
                                            return true;
                                        }else
                                        {
                                            response = "La habilitacion indicada ya se encuentra selecionada"; //Mnesaje que se muestra si ya habia seleccionado el indice con anterioridad.
                                            return true;
                                        }
                                    }else
                                    {  
                                        response = "El indice ingresado no es valido"; //Mnesaje que se muestra si el numero ingresado no corresponde.
                                        return true;
                                    }
                                }else
                                {
                                    response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir la primera habilitacion)"; //Mnesaje que se muetra si no ingreso un numero.
                                    return true;
                                }
                            }
                        }
                        response = "Ahora ingrese su especializacion:"; //Mnesaje que indica la proxima informacion a incluir.
                        userData.Add(""); //Agrego a la lista de informacion un dato hefimero, para que salte al proimo case la siguinete vez que entre al handler.
                        return true;
                    case 7: //Si la lista de informacion tiene siete elementos, signifca que el usurio, que se quiere registrar como emprendedor, ingreso su especializacio.
                        if (!message.Text.Contains("?")) //Regulo si el mensaje contiene caracteres no validos.
                        {
                            userData.RemoveAt(6);//Remueveo el dato hefimero y agrego el dato ingresado por el usuario.
                            userData.Add(message.Text);
                        }else
                        {
                            response = "Por favor ingrese un dato valido"; //Mnesjae que se muestra si el mensaje contiene caracteres no validos.
                            return true;
                        }
                        mensaje = new StringBuilder();
                        string[] habilitaciones = userData[5].Split("-"); //Construyo parte del mensaje a mostrar.
                        foreach(string item in habilitaciones)
                        {
                            if (int.TryParse(item, out number))
                            {
                                mensaje.Append($"\n-{AppLogic.Instance.Qualifications[number-1].QualificationName}");
                            }
                        }
                        userData.Add(" ");//Agrego otro dato hefimero para que salte al proximo case.
                        response = $"Por favor, veo si los datos ingresados son correctos.\nNombre: {userData[1]}\nUbicacion: {userData[2]}\nRubro: {AppLogic.Instance.Rubros[Convert.ToInt32(userData[3])].RubroName}\nMail de contacto: {userData[4]}\nHabilitaciones: {mensaje}\nEspecializacion: {userData[6]}\n\nSi son correctos ingrese '/si', en caso contrario '/no'"; //Envio un mensaje con la informacion que ingreso el usuario para que confirme su registro.
                        return true;
                    case 8:
                        if (message.Text.ToLower().Replace(" ","").Equals("/si")) //Verifico si quiere continuar con el registro.
                        {
                            string[] habilitacionesLista = userData[5].Split("-");
                            List<Qualifications> lista = new List<Qualifications>();
                            foreach(string item in habilitacionesLista) //Construyo, a partir de los datos que se alamcenaron en la lista de informacion (como los indices), los objetos necesarios para crear un emprendedor.
                            {
                                if (int.TryParse(item, out number))
                                {
                                    lista.Add(AppLogic.Instance.Qualifications[number-1]);
                                }
                            }
                            
                            AppLogic.Instance.RegisterEntrepreneurs(message.Id, userData[1], userData[2], AppLogic.Instance.Rubros[Convert.ToInt32(userData[3])], lista, userData[6], userData[4]); //Utilizo a un metod de la logica, que registra a un emprendedor.
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id); //Remuevo al usuario del diccionario de usuarios que estan interactuando con la aplicacion, porque ya termino su registro.
                            response = "Usted se a registrado con exito"; //Mnesaje de confirmacion.
                            return true;
                        }if(message.Text.ToLower().Replace(" ","").Equals("/no")) //Verifico si el usuario quiere cancelar su registro.
                        {
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id); //Remuevo al usuario del diccionario de usuarios que estan interactuando con la aplicacion.
                            response = "Se procedera a eliminar todos los datos ingresados.\n\nEn el caso de querer volver a registrarse, por favor use el comando '/Registrar'."; //Mensaje de confirmacion.
                            return true;
                        }
                        response = "Dato incorrecto\n Por favor ingrese '/si' o '/no'."; //Mnesaje que se envia si el usuario no ingreso el dato esperado.
                    return true;
                }
                
            }
            response = string.Empty;
            return false; //Si el programa llego a este punto, significa que el mesnje ingresado no es para este handler.
        }
    }
}