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
                    response = "Indique como quiere buscar ofertas\n/1 - Nombre\n/2 - Ubicacion\n/3 - Clasificación";
                    List<List<string>> lista = new List<List<string>>() {new List<string>(),new List<string>()};
                    DataUserContainer.Instance.UserDataHistory.Add(message.Id,lista);
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
                    response = "Indique la clasificacion de ofertas deseadas";
                    return true;
                }if (DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 0)
                {
                    response = "Debe ingresar /si o /no";
                    return true;
                }
                string position = DataUserContainer.Instance.UserDataHistory[message.Id][1][0];
                List<string> userData = DataUserContainer.Instance.UserDataHistory[message.Id][1];
                if(userData[0].Equals("Nombre") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 1)
                {
                    if(AppLogic.Instance.SearchOfferByKeywords(message.Text) != null)
                    {
                        List<IOffer> list = AppLogic.Instance.SearchOfferByKeywords(message.Text);
                        StringBuilder sb = new StringBuilder($"Ofertas de {message.Text} disponibles - Escriba el número de la oferta que desea comprar");
                        foreach (IOffer item in list)
                        {
                            sb.Append($"{item.Id} - {item.Product.Classification} - cantidad: {item.Product.Quantity} - precio unitario: {item.Product.Price} - ubicación: {item.Product.Ubication} - fecha de publicación: {item.DatePublished}\n");
                        }
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/comprar");
                        response = sb.ToString();
                        return true;
                    }
                }
                if(userData[0].Equals("Ubicacion") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 1)
                {
                    if(AppLogic.Instance.SearchOfferByKeywords(message.Text) != null)
                    {
                        List<IOffer> list = AppLogic.Instance.SearchOfferByUbication(message.Text);
                        StringBuilder sb = new StringBuilder($"Ofertas en {message.Text} disponibles - Escriba el número de la oferta que desea comprar");
                        foreach (IOffer item in list)
                        {
                            sb.Append($"{item.Id} - {item.Product.Classification} - cantidad: {item.Product.Quantity} - precio unitario: {item.Product.Price} - ubicación: {item.Product.Ubication} - fecha de publicación: {item.DatePublished}\n");
                        }
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/comprar");
                        response = sb.ToString();
                        return true;
                    }
                }
                if(userData[0].Equals("Clasificacion") && DataUserContainer.Instance.UserDataHistory[message.Id][1].Count == 1)
                {
                    if(AppLogic.Instance.SearchOfferByKeywords(message.Text) != null)
                    {
                        List<IOffer> list = AppLogic.Instance.SearchOfferByType(message.Text);
                        StringBuilder sb = new StringBuilder($"Ofertas de tipo {message.Text} disponibles - Escriba el número de la oferta que desea comprar");
                        foreach (IOffer item in list)
                        {
                            sb.Append($"{item.Id} - {item.Product.Classification.Category} - cantidad: {item.Product.Quantity} - precio unitario: {item.Product.Price} - ubicación: {item.Product.Ubication} - fecha de publicación: {item.DatePublished}\n");
                        }
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Add("/comprar");
                        response = sb.ToString();
                        return true;
                    }
                }
                if(userData[1].Equals("/comprar"))
                {
                    int indice;
                    if(int.TryParse(comando[1],out indice))
                    {
                        IOffer offer = AppLogic.Instance.GetOffer(indice);
                        if(offer == null)
                        {
                            response = "Esa oferta no existe";
                            return true;
                        }
                        Emprendedor emp = AppLogic.Instance.GetEmprendedor(message.Id);
                        AppLogic.Instance.AccepOffer(emp,offer);
                        response = "Oferta comprada con exito!";
                        DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                    }
                    else
                    {
                        response = "Seleccione un indice valido";
                    }
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}