using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// Hereda de Base Handler.
    /// </summary>
    public class PurchasedOfferHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PurchasedOfferHandler"/>. Esta clase procesa si la persona que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public PurchasedOfferHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/misofertasaceptadas", "/oferta" };
        }

        /// <summary>
        /// Procesa el mensaje "/misofertasaceptadas" y "/oferta" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (message.Text.ToLower().Replace(" ", "").Contains(this.Keywords[0]) && message.Text.ToLower().Split(" ")[0].Equals("/misofertasaceptadas"))
            {
                if (!DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    bool comandoSolo = false;
                    int number = 0;
                    string textoRecivido = message.Text.ToLower().Trim(' ');
                    string[] comando = textoRecivido.Split(" ");
                    StringBuilder mensaje = new StringBuilder();
                    List<List<string>> lista = new List<List<string>>() { new List<string>(), new List<string>() };
                    DataUserContainer.Instance.UserDataHistory.Add(message.Id, lista);
                    DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/misofertasaceptadas");

                    if (comando.Count() <= 2)
                    {
                        if (comando.Count() == 2)
                        {
                            if (!int.TryParse(comando[1], out number))
                            {
                                response = "Debe ingresar /misofertasaceptadas o /misofertasaceptadas (número) para obtener sus ofertas aceptadas.";
                                return true;
                            }
                            if (number < 1)
                            {
                                response = "Debe ingresar un número mayor o igual a 1 en el periodo de dias";
                                return true;
                            }
                            DataUserContainer.Instance.UserDataHistory[message.Id][1].Add(comando[1]);
                        }
                        else
                        {
                            comandoSolo = true;
                        }
                    }
                    else
                    {
                        response = "Comando no valido\n\nSi quiere ver las ofertas aceptadas puede ingresar /misofertasaceptadas y obtener todas sus ofertas aceptadas o /mis ofertas aceptadas (dia) para obtener las ofertas aceptadas desde la actualidad hasta el (dias) atras. (Ej: si ingresa '/misofertasaceptadas 2' obtendrá todas las ofertas aceptadas en los ultimos dos dias.";
                        return true;
                    }
                    List<IOffer> listaOfertas = null;
                    int index = 1;
                    if (AppLogic.Instance.GetCompany(message.Id) != null)
                    {
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Insert(0, "company");
                        mensaje.Append($"Estas son las ofertas publicadas, que fueron aceptadas por emprendedores{(comandoSolo ? "" : " en los ultimos " + number.ToString() + " dias")}:\n");
                        if (comandoSolo)
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetCompany(message.Id));
                        }
                        else
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetCompany(message.Id), number);
                        }
                        foreach (IOffer item in listaOfertas)
                        {
                            IList<PurchaseData> purchaseData = new List<PurchaseData>();
                            if (comandoSolo)
                            {
                                purchaseData = item.PurchesedData;
                            }
                            else
                            {
                                purchaseData = item.GetPeriodTimeOffersAcceptedData(number);
                            }
                            mensaje.Append($"\n{index++}-->{item.Product.Quantity} Kilos de {item.Product.Classification.Category} a un precio de {item.Product.Price}$ aceptada el {purchaseData[0].PurchaseDate} por {purchaseData[0].Buyer.Name}");
                        }
                    }
                    else
                    {
                        DataUserContainer.Instance.UserDataHistory[message.Id][1].Insert(0, "emprendedor");
                        mensaje.Append($"Estas son las ofertas que acepto{(comandoSolo ? "" : " en los ultimos " + number.ToString() + " dias")}:\n");
                        if (comandoSolo)
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetEmprendedor(message.Id));
                        }
                        else
                        {
                            listaOfertas = AppLogic.Instance.GetOffersAccepted(AppLogic.Instance.GetEmprendedor(message.Id), number);
                        }
                        foreach (IOffer item in listaOfertas)
                        {
                            IList<PurchaseData> purchaseData = new List<PurchaseData>();
                            if (comandoSolo)
                            {
                                purchaseData = item.GetEntrepreneursPurchaseData(AppLogic.Instance.GetEmprendedor(message.Id));
                            }
                            else
                            {
                                purchaseData = item.GetPeriodTimeOffersAcceptedData(number, AppLogic.Instance.GetEmprendedor(message.Id));
                            }
                            mensaje.Append($"\n{index++}-->{item.Product.Quantity} Kilos de {item.Product.Classification.Category} a un precio de {item.Product.Price} aceptada el {purchaseData[0].PurchaseDate}");
                        }
                    }
                    DataUserContainer.Instance.UserOfferDataSelection.Add(message.Id, listaOfertas);
                    response = mensaje.ToString();
                }
                else
                {
                    response = "No puede realizar esta acción en este momento";
                }
                return true;
            }
            if (DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id) && DataUserContainer.Instance.UserDataHistory[message.Id][0][0].Equals("/misofertasaceptadas"))
            {
                List<string> userData = DataUserContainer.Instance.UserDataHistory[message.Id][1];
                string mensaje = message.Text.Trim(' ');
                string[] comando = mensaje.ToLower().Split(" ");
                if (DataUserContainer.Instance.UserDataHistory[message.Id][0].Count == 1)
                {
                    if (comando.Count() == 2)
                    {
                        if (this.Keywords.Contains(comando[0]))
                        {
                            int number;
                            if (int.TryParse(comando[1], out number))
                            {
                                if (DataUserContainer.Instance.UserOfferDataSelection[message.Id].Count - number >= 0 && number != 0 && number > 0)
                                {
                                    IOffer oferta = DataUserContainer.Instance.UserOfferDataSelection[message.Id][number - 1];
                                    userData.Add(comando[1]);
                                    StringBuilder mensajeHabilitaciones = new StringBuilder();
                                    StringBuilder mensajeKeyWords = new StringBuilder();
                                    foreach (Qualifications item in oferta.Qualifications)
                                    {
                                        mensajeHabilitaciones.Append($"\n--{item.QualificationName}");
                                    }
                                    foreach (string item in oferta.KeyWords)
                                    {
                                        mensajeKeyWords.Append($"-{item}- ");
                                    }
                                    StringBuilder mensajeCompraData = new StringBuilder();
                                    IList<PurchaseData> datosDeCompra = new List<PurchaseData>();
                                    if (userData[0].Equals("company"))
                                    {
                                        int index = 1;
                                        if (userData.Count == 2)
                                        {
                                            datosDeCompra = oferta.GetPeriodTimeOffersAcceptedData(Convert.ToInt32(userData[1]));
                                        }
                                        else
                                        {
                                            datosDeCompra = oferta.PurchesedData;
                                        }
                                        foreach (PurchaseData item in datosDeCompra)
                                        {
                                            mensajeCompraData.Append($"\n{index++}--{item.Buyer} la acepto el {item.PurchaseDate}");
                                        }
                                        DataUserContainer.Instance.UserDataHistory[message.Id][0].Add("/oferta");
                                        response = $"Oferta {number}.\nPublicada el: {oferta.DatePublished}\n\nClasificación del producto: {oferta.Product.Classification.Category}\nCantidad del producto: {oferta.Product.Quantity}\nPrecio de compra: {oferta.Product.Price}\nUbicación del producto: {oferta.Product.Ubication}\nRequerimientos necesarios:{mensajeHabilitaciones}\nPalabras claves asociadas: {mensajeKeyWords}\n\nEmprendedor/es que la aceptaron: {mensajeCompraData}\n\n Usted puede seleccionar al comprador indicando su indice para obtener más detalles, o utilizar /Cancel para salir.";
                                        return true;
                                    }
                                    else
                                    {
                                        if (userData.Count > 2)
                                        {
                                            datosDeCompra = oferta.GetPeriodTimeOffersAcceptedData(Convert.ToInt32(userData[1]), AppLogic.Instance.GetEmprendedor(message.Id));
                                        }
                                        else
                                        {
                                            datosDeCompra = oferta.GetEntrepreneursPurchaseData(AppLogic.Instance.GetEmprendedor(message.Id));
                                        }
                                        foreach (PurchaseData item in datosDeCompra)
                                        {
                                            mensajeCompraData.Append($"\n{item.PurchaseDate}");
                                        }
                                        DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                        DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                                        response = $"Oferta {number}.\nPublicada el: {oferta.DatePublished}\n\nClasificación del producto: {oferta.Product.Classification.Category}\nCantidad del producto: {oferta.Product.Quantity}\nPrecio de compra: {oferta.Product.Price}\nUbicación del producto: {oferta.Product.Ubication}\nRequerimientos necesarios:{mensajeHabilitaciones}\nPalabras claves asociadas: {mensajeKeyWords}\nFecha de compra: {mensajeCompraData}";
                                        return true;
                                    }
                                }
                                else
                                {
                                    response = "Debe ingresar un número valido";
                                    return true;
                                }
                            }
                            else
                            {
                                response = "Debe ingresar el comando y un número";
                                return true;
                            }
                        }
                        else
                        {
                            response = "El comando ingresado no es valido, recuerde ingresar /oferta (indice de la oferta) para ver los detalles de compra de una oferta";
                            return true;
                        }
                    }
                    else
                    {
                        response = "Para ver una oferta en especifico debe ingresar el comando /oferta (indice de la oferta)";
                        return true;
                    }
                }
                else
                {
                    if (comando.Count() == 1)
                    {
                        int number = 0;
                        if (int.TryParse(comando[0], out number))
                        {
                            IList<PurchaseData> datosDeCompra = new List<PurchaseData>();
                            if (userData.Count == 3)
                            {
                                datosDeCompra = DataUserContainer.Instance.UserOfferDataSelection[message.Id][Convert.ToInt32(userData[userData.Count - 1])].GetPeriodTimeOffersAcceptedData(Convert.ToInt32(userData[1]));
                            }
                            else
                            {
                                datosDeCompra = DataUserContainer.Instance.UserOfferDataSelection[message.Id][Convert.ToInt32(userData[userData.Count - 1])].PurchesedData;
                            }
                            if (datosDeCompra.Count - number >= 0)
                            {
                                StringBuilder mensajeHabilitaciones = new StringBuilder();
                                foreach (Qualifications item in datosDeCompra[number - 1].Buyer.Qualifications)
                                {
                                    mensajeHabilitaciones.Append($"\n-{item.QualificationName}");
                                }
                                DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                                DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                                response = $"Comprador {number}\n\nNombre: {datosDeCompra[number - 1].Buyer.Name}\nRubo al que pertenece: {datosDeCompra[number - 1].Buyer.Rubro}\nUbicación: {datosDeCompra[number - 1].Buyer.Ubication}\nContacto: {datosDeCompra[number - 1].Buyer.User_Contact}\nHabilitaciones que posee: {mensajeHabilitaciones}\nFecha de compra: {datosDeCompra[number - 1].PurchaseDate}.";
                            }
                            else
                            {
                                response = "Debe ingresar un número valido";
                            }
                        }
                        else
                        {
                            response = "Debe ingresar un número que concuerde con algun índice";
                        }
                    }
                    else
                    {
                        response = "Debe ingresar solo el índice del comprador que quiere ver";
                    }
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}