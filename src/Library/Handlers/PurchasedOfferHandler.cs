using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class PurchasedOfferHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PurchasedOfferHandler"/>. Esta clase procesa si la perosna que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public PurchasedOfferHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/misofertasaceptadas"};
        }

        /// <summary>
        /// Procesa el mensaje "/registrar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (message.Text.ToLower().Replace(" ","").Contains(this.Keywords[0]) && message.Text.ToLower().Split(" ")[0].Equals("/misofertasaceptadas"))
            {
                if(!DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    bool comandoSolo = false;
                    int number = 0;
                    string textoRecivido = message.Text.ToLower().Trim(' ');
                    string[] comando = textoRecivido.Split(" ");
                    StringBuilder mensaje = new StringBuilder();
                    if (comando.Count() <= 2)
                    {
                        if (comando.Count() == 2)
                        {
                            if(!int.TryParse(comando[1], out number))
                            {
                                response = "Debe ingresar /misofertasaceptadas o /misofertasaceptadas (numero) para obtener sus ofertas aceptadas.";
                                return true;
                            }if (number >=1)
                            {
                                response = "Debe ingresar un numero mayor o igual a 1 en el periodo de dedias";
                                return true;
                            }
                        }else
                        {
                            comandoSolo = true;
                        }
                    }else
                    {
                        response = "Comando no valido\n\nSi quiere ver las ofertas aceptadas puede ingresar /misofertasaceptadas y obtener todas sus ofertas aceptadas o /mis ofertas aceptadas (dia) para obtener las ofertas aceptadas desde la actualidad hasta el (dias) atras. (Ej: si ingresa '/misofertasaceptadas 2' obtendra todas las ofertas aceptadas en los ultimos dos dias.";
                        return true;
                    }
                    List<IOffer> listaOfertas = null;
                    if (AppLogic.Instance.GetCompany(message.Id) != null)
                    {
                        mensaje.Append($"Estas son las ofertas que publicó, que fueron aceptadas por emprendedores{(comandoSolo ? "" : " en los ultimos " + number.ToString() + " dias")}:\n");
                        if (comandoSolo)
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetCompany(message.Id));
                        }else
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetCompany(message.Id),number);
                        }
                    }else
                    {
                        mensaje.Append($"Estas son las ofertas que acepto{(comandoSolo ? "" : " en los ultimos " + number.ToString() + " dias")}:\n");
                        if (comandoSolo)
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetEmprendedor(message.Id));
                        }else
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetEmprendedor(message.Id),number);
                        }
                    }
                        foreach(IOffer item in listaOfertas)
                        {
                            PurchaseData datosDeCompra = null;
                            if (comandoSolo)
                            {
                                item.GetPeriodTimeOffersAcceptedData(-1, out datosDeCompra);
                            }
                            else
                            {
                                item.GetPeriodTimeOffersAcceptedData(number, out datosDeCompra);
                            }
                            mensaje.Append($"\n-->{item.Product.Quantity} Kilos de {item.Product.Classification.Category} a un precio de {item.Product.Price} aceptada el {datosDeCompra.PurchaseDate}{(AppLogic.Instance.GetCompany(message.Id) != null ? $" por {datosDeCompra.Buyer.Name}" : "")}");
                        }
                    response = mensaje.ToString();
                }else
                {
                    response = "No puede realizar esta ccion en este momento";
                }
                return true;
            }
            response = string.Empty;
            return false;
        }
    }
}