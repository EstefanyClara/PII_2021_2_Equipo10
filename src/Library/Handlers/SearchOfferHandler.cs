using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Nito.AsyncEx;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class SearchOfferHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SearchOfferHandler"/>. Esta clase procesa el mensaje public, para publicar una oferta.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public SearchOfferHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/buscar"};
        }

        /// <summary>
        /// Procesa el mensaje "/registrar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            string[] comando = message.Text.ToLower().Split(" ");
            if (this.Keywords.Contains(message.Text.ToLower().Replace(" ","")))
            {
                if(AppLogic.Instance.GetEmprendedor(message.Id) == null)
                {
                    response = "Solo aquellos registrados como emprendedor pueden buscar ofertas";
                    return true;
                }
                if(!DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    response = "Indique como quiere buscar ofertas\n/1 - Palabra clave.\n/2 - Ubicacíon\n/3 - Clasificación";
                    List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
                    DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);
                    DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/buscar");
                    return true;
                }else
                {
                    if (DataUserContainer.Instance.UserDataHistory[message.Id][0][0].Equals("/buscar"))
                    {
                        response = "Usted ya esta en proceso de busqueda";
                        return true;
                    }else
                    {
                        response = string.Empty;
                        return false;
                    }
                }
            }if(DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id) && DataUserContainer.Instance.UserDataHistory[message.Id][0][0].Equals("/buscar"))
            {
                if ((message.Text.ToLower().Replace(" ","").Equals("/1") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0))
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("Nombre");
                    response = "Indique nombre de ofertas deseadas";
                    return true;
                }
                if ((message.Text.ToLower().Replace(" ","").Equals("/2") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0))
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("Ubicacion");
                    response = "Indique la ubicacion de ofertas deseadas";
                    return true;
                }
                if ((message.Text.ToLower().Replace(" ","").Equals("/3") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0))
                {
                    DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("Clasificacion");
                    StringBuilder mensaje = new StringBuilder();
                    mensaje.Append("Indique la clasificación de ofertas deseadas indicando su indice.");
                    int indice = 1;
                    foreach (Classification item in AppLogic.Instance.Classifications)
                    {
                        mensaje.Append($"{indice++}-{item.Category}\n");
                    }
                    response = mensaje.ToString();
                    return true;
                }if (DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0)
                {
                    response = "Debe ingresar /1, /2, o /3";
                    return true;
                }
                string position = DataUserContainer.Instance.UserDataHistory[message.Id][1][0];
                List<string> userData = DataUserContainer.Instance.UserDataHistory[message.Id][1];
                int index = 1;
                if(userData[0].Equals("Nombre") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 1)
                {
                    List<IOffer> list = AppLogic.Instance.SearchOfferByKeywords(message.Text.Trim(' '));
                    if(list.Count != 0)
                    {
                        DataUserContainer.Instance.UserOfferDataSelection.Add(message.Id, list);
                        DataUserContainer.Instance.UserOfferDataSelection[message.Id] = list;
                        StringBuilder sb = new StringBuilder($"Ofertas de {message.Text} disponibles - Escriba el número de la oferta para verla mas a detalle");
                        foreach (IOffer item in list)
                        {
                            sb.Append($"\n{index++} - {item.Product.Classification.Category} - cantidad: {item.Product.Quantity} Kilos - precio unitario: {item.Product.Price} - ubicación: {item.Product.Ubication} - fecha de publicación: {item.DatePublished}");
                        }
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/comprar");
                        response = sb.ToString();
                        return true;
                    }else
                    {
                        DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                        DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                        response = "No se encontraron ofertas con la palabra clave indicada.";
                    }
                }
                if(userData[0].Equals("Ubicacion") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 1)
                {
                    List<IOffer> list = AppLogic.Instance.SearchOfferByUbication(message.Text.Trim(' '));
                    if(list.Count != 0)
                    {
                        DataUserContainer.Instance.UserOfferDataSelection.Add(message.Id, list);
                        DataUserContainer.Instance.UserOfferDataSelection[message.Id] = list;
                        StringBuilder sb = new StringBuilder($"Ofertas en {message.Text} disponibles - Escriba el número de la oferta verla mas a detalle");
                        foreach (IOffer item in list)
                        {
                            sb.Append($"\n{index++} - {item.Product.Classification.Category} - cantidad: {item.Product.Quantity} - precio unitario: {item.Product.Price} - ubicación: {item.Product.Ubication} - fecha de publicación: {item.DatePublished}");
                        }
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/comprar");
                        response = sb.ToString();
                        return true;
                    }else
                    {
                        DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                        DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                        response = "No se encontraron ofertas con la ubicacion indicada.";
                        return true;
                    }
                }
                if(userData[0].Equals("Clasificacion") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 1)
                {
                    int number;
                    if (int.TryParse(message.Text, out number))
                    {
                        if (AppLogic.Instance.Classifications.Count - number >= 0)
                        {
                            List<IOffer> list = AppLogic.Instance.SearchOfferByType(AppLogic.Instance.Classifications[number-1].Category);
                            if(list.Count != 0)
                            {
                                DataUserContainer.Instance.UserOfferDataSelection.Add(message.Id, list);
                                DataUserContainer.Instance.UserOfferDataSelection[message.Id] = list;
                                StringBuilder sb = new StringBuilder($"Ofertas de tipo {message.Text} disponibles - Escriba el número de la oferta verla mas a detalle");
                                foreach (IOffer item in list)
                                {
                                    sb.Append($"\n{index++} - {item.Product.Classification.Category} - cantidad: {item.Product.Quantity} - precio unitario: {item.Product.Price} - ubicación: {item.Product.Ubication} - fecha de publicación: {item.DatePublished}");
                                }
                                DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/comprar");
                                response = sb.ToString();
                                return true;
                            }else
                            {
                                DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                                response = "No se encontaron ofertan con dicha clasificacion.";
                                return true;
                            }  
                        }else
                        {
                            response = "Número invalido";
                            return true;
                        }
                    }else
                    {
                        response = "El dato ingresado no es valido\nPor favor, revise que haya ingresado un número (Ej:'1' Para elegir la primera clasificacion)";
                        return true;
                    }
                }
                switch(userData.Count)
                {
                    case 2:
                        int indice;
                        if(int.TryParse(message.Text.Trim(' '), out indice))
                        {
                            if(DataUserContainer.Instance.UserOfferDataSelection[message.Id].Count - indice < 0)
                            {
                                response = "Esa oferta no existe";
                                return true;
                            }else
                            {
                                IOffer oferta = DataUserContainer.Instance.UserOfferDataSelection[message.Id][indice-1];
                                StringBuilder mensaje = new StringBuilder();
                                foreach(Qualifications item in oferta.Qualifications)
                                {
                                    mensaje.Append($"-{item.QualificationName}");
                                }
                                userData.Add(indice.ToString());
                                response = $"Oferta {indice}.\n\nClasificacion: {oferta.Product.Classification.Category}\nPrecio: {oferta.Product.Price}\nCantidad: {oferta.Product.Quantity}\nUbicacion: {oferta.Product.Ubication}\nHabilitaciones necesarias: {mensaje}\n\n\nPuede utilizar /Distancia para obetenr un mapa de la oferta junto con su distancia actual\nPuede hacer uso de /map para obtener un mapa de la ubicacion de la oferta.";
                                return true;
                            }
                        }else
                        {
                            response = "Numero no valido";
                            return true;
                        }
                    case 3:
                        Emprendedor emp = AppLogic.Instance.GetEmprendedor(message.Id);
                        IOffer offer = DataUserContainer.Instance.UserOfferDataSelection[message.Id][Convert.ToInt32(userData[2])-1];
                        if (message.Text.ToLower().Trim(' ').Equals("/distancia"))
                        {
                            AppLogic.Instance.ObteinOfferDistance(emp, offer);
                            AsyncContext.Run(() => message.SendProfileImage(AppLogic.Instance.ObteinOfferDistance(emp, offer), @"route.png"));
                            response = string.Empty;
                            return true;
                        }if(message.Text.ToLower().Trim(' ').Equals("/map"))
                        {
                            AppLogic.Instance.ObteinOfferMap(offer);
                            AsyncContext.Run(() => message.SendProfileImage($"Este es el mapa de la oferta que esta en {offer.Product.Ubication}", @"map.png"));
                            response = string.Empty;
                            return true;
                        }if (message.Text.ToLower().Trim(' ').Equals("/comprar"))
                        {
                            if(AppLogic.Instance.AccepOffer(emp, DataUserContainer.Instance.UserOfferDataSelection[message.Id][Convert.ToInt32(userData[2])-1]))
                            {
                                response = "Oferta comprada con exito!";
                                DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                            }else
                            {
                                response = "Esta oferta ya no esta disponible, o usted no posee las habilitaciones necesarias.";
                                DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                            }
                        }
                        else
                        {
                            response = "Comando no valido.";
                        }
                        return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}