using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class PublicOfferHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PublicOfferHandler"/>. Esta clase procesa el mensaje public, para publicar una oferta.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public PublicOfferHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/public"};
        }

        /// <summary>
        /// Procesa el mensaje "/registrar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.Keywords.Contains(message.Text.ToLower().Replace(" ","")))
            {
                bool esCompania = false;
                foreach(Company item in AppLogic.Instance.Companies)
                {
                    if (item.User_Id.Equals(message.Id))
                    {
                        esCompania = true;
                    }
                }
                if(!esCompania)
                {
                    response = "Solo aquellos registrados como compania pueden publicar una oferta";
                    return true;
                }
                if(!DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    response = "¿La oferta que desea publicar es constnate?(/si o /no)\n\nUna oferta constante son aquellas que siempre estan disponibles y las pueden aceptar varios emprendedores, mientras que las no constante solo la puede aceptar un emprendedor";
                    List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
                    DataUserContainer.Instance.UserDataHistory.Add(message.Id,lista);
                    DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/public");
                    return true;
                }else
                {
                    response = "Usted ya esta en proceso de publicacion";
                    return true;
                }
            }if(DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id) && DataUserContainer.Instance.UserDataHistory[message.Id][0][0].Equals("/public"))
            {
                if ((message.Text.ToLower().Replace(" ","").Equals("/si") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0) | (message.Text.ToLower().Replace(" ","").Equals("/no") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0))
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add(message.Text.ToLower().Replace(" ",""));
                    StringBuilder mensaje = new StringBuilder();
                    mensaje.Append("A continuacion ingrese la clasificacion del producto.\n\nEliga entre las habilitadas por la aplicacion indicando su indice.\n");
                    int indice = 0;
                    foreach (Classification item in AppLogic.Instance.Classifications)
                    {
                        indice++;
                        mensaje.Append($"{indice}-{item.Category}\n");
                    }
                    response = mensaje.ToString();
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
                        int number;
                        if (int.TryParse(message.Text, out number))
                        {
                            if (AppLogic.Instance.Classifications.Count - number >= 0)
                            {
                                number--;
                                userData.Add(number.ToString());
                                response = "Ahora ingrese la cantida en del producto (en kilogramos)";
                            }else
                            {
                                response = "Numero invalido";
                            }
                        }else
                        {
                            response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir la primera clasificacion)";
                        }
                    return true;
                    case 2:
                        float cantidad;
                        if (float.TryParse(message.Text, out cantidad))
                        {
                            userData.Add(cantidad.ToString());
                            response = "Ahora ingrese el costo del producto (en pesos uruguayos)";
                        }else
                        {
                            response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir el primer rubro)";
                        }
                    return true;
                    case 3:
                        if (float.TryParse(message.Text, out cantidad))
                        {
                            userData.Add(cantidad.ToString());
                            response = "Ahora ingrese la ubicacion del producto";
                        }else
                        {
                            response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir el primer rubro)";
                        }
                    return true;
                    case 4:
                        if (!message.Text.Contains("?"))
                        {
                            userData.Add(message.Text); //La ubicacion del producto
                            StringBuilder mensaje = new StringBuilder();
                            mensaje.Append("A continuacion ingrese las habilitaciones que posee el producto.\n\nEliga entre los habilitadoas por la aplicacion indicando su indice (puede elegir mas de una).\n");
                            int index = 0;
                            foreach (Qualifications item in AppLogic.Instance.Qualifications)
                            {
                                index++;
                                mensaje.Append($"{index}-{item.QualificationName}\n");
                            }
                            response = mensaje.ToString();
                        }else
                        {
                            response = "Por favor ingrese un dato valido";
                        }
                    return true;
                    case 5:
                        if (int.TryParse(message.Text, out number))
                        {
                            if (AppLogic.Instance.Qualifications.Count - number >= 0 )
                            {
                                userData.Add("-" + number.ToString());
                                response = "Se ha agregado la habilitacion";
                                return true;
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
                    case 6:
                        if(!message.Text.ToLower().Replace(" ","").Equals("/stop"))
                        {
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
                                        response = "La habilitacion indicada ya se encuentra sellecionada";
                                        return true;
                                    }                           
                                }else{  
                                    response = "El indice ingresado no es valido";
                                    return true;
                                }
                            }else
                            {
                                response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un numero (Ej:'1' Para elegir la primera habilitacion)";
                                return true;
                            }
                        }if(userData[5].Contains("-"))
                        {
                            response = "Ahora ingrese las palabras claves del producto\n\nEstas palabras las usaran los emprendedores a la hora de buscar ofertas\n\nPuede ingresar una o varias separdas por ','.";
                            userData.Add("P");
                            return true;
                        }
                        response = "Tiene que ingresar al menos una habilitacion";
                    return true;
                    case 7:
                        if(!message.Text.ToLower().Replace(" ","").Equals("/stop") )
                        {
                            if (!message.Text.Contains("?"))
                            {
                                if (!userData[6].Contains(","))
                                {
                                    userData.RemoveAt(6);
                                    userData.Add(message.Text+",");
                                    response = "Se ha/n agregado la/s palabra clave";
                                    return true;
                                }else
                                {
                                    string repetidas = "Las siguientes palabras estan repetidas: |";
                                    string noRepetidas = string.Empty;
                                    string[] palabraIngresada = message.Text.Replace(".",",").Split(",");
                                    foreach(string item in palabraIngresada)
                                    {
                                        item.Trim(' ');
                                        if (userData[6].Contains(item))
                                        {
                                            Console.WriteLine(item + "2");
                                            repetidas = repetidas + item + "|";
                                        }else
                                        {
                                            noRepetidas = noRepetidas + "," + item;
                                        }
                                    }
                                        string words = userData[6];
                                        words = words + noRepetidas;
                                        userData.RemoveAt(6);
                                        userData.Add(words);
                                        if(repetidas != "Las siguientes palabras estan repetidas: |")
                                        {
                                            response = repetidas + "\n\nLas palabras repitadas no se agregaran";
                                            return true;
                                        }
                                        response = "Se ha/n agregado la/s palabra clave";
                                        return true;
                                }
                            }else
                            {
                                response = "Por favor ingrese un dato valido";
                                return true;
                            }
                        }if (!userData[6].Contains(","))
                        {
                            response = "Debe ingresar al menos una palabra clave";
                            return true;
                        }else
                        {
                            userData.Add(" ");
                            StringBuilder mensaje = new StringBuilder();
                            string[] habilitaciones = userData[5].Split("-");
                            foreach(string item in habilitaciones)
                            {
                                if (int.TryParse(item, out number))
                                {
                                    mensaje.Append($"\n-{AppLogic.Instance.Qualifications[number-1].QualificationName}");
                                }
                            }
                            StringBuilder mensajePalabras = new StringBuilder();
                            string[] words = userData[6].Replace(","," ").Split(" ");
                            foreach(string item in words)
                            {
                                mensajePalabras.Append($"|{item}|");
                            }
                            response = $"Por favor, veo si los datos ingresados son correctos.\nClasificacion del producto: {userData[1]}\nCantidad: {userData[2]} Kilogramos\nPrecio: {userData[3]}$\nUbicacion: {userData[4]}\nHabilitaciones necesarias: {mensaje}\nPalabras clave: {mensajePalabras}\n\nSi son correctos ingrese '/si', en caso contrario '/no'";
                            return true;
                        }
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
                            ArrayList words = new ArrayList(){userData[6].Replace(","," ").Split(" ")};
                            Company compania = null;
                            foreach (Company item in AppLogic.Instance.Companies)
                            {
                                if (item.User_Id == message.Id)
                                {
                                    compania = item;
                                    break;
                                }
                            }
                            if (userData[0].Equals("/si"))
                            {
                                AppLogic.Instance.PublicConstantOffer(compania,AppLogic.Instance.Classifications[Convert.ToInt32(userData[1])],Convert.ToDouble(userData[2]),Convert.ToDouble(userData[3]),userData[4],lista,words);
                            }if (userData[0].Equals("/no"))
                            {
                                AppLogic.Instance.PublicNonConstantOffer(compania,AppLogic.Instance.Classifications[Convert.ToInt32(userData[1])],Convert.ToDouble(userData[2]),Convert.ToDouble(userData[3]),userData[4],lista,words);
                            }
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                            response = "Usted a publicado la oferta con exito";
                            return true;
                        }if(message.Text.ToLower().Replace(" ","").Equals("/no"))
                        {
                            DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                            response = "Se procedera a eliminar todos los datos ingresados.\n\nEn el caso de querer volver a querer publicar una oferta, por favor use el comando '/Public'.";
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