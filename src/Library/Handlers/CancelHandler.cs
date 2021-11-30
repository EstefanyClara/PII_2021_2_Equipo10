using System;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class CancelHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AutorizationHandler"/>. Esta clase procesa si la persona que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CancelHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/cancel" };
        }

        /// <summary>
        /// Procesa el mensaje "/registrar" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.Keywords.Contains(message.Text.ToLower().Replace(" ", "")))
            {
                if (DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    DataUserContainer.Instance.UserDataHistory.Remove(message.Id);
                    if (DataUserContainer.Instance.UserOfferDataSelection.Keys.Contains(message.Id))
                    {
                        DataUserContainer.Instance.UserOfferDataSelection.Remove(message.Id);
                    }
                    response = "Regresando al estado inicial...";
                }
                else
                {
                    response = "Usted no se encuentra en ningún estado especifico.";
                }
                return true;
            }
            response = string.Empty;
            return false;
        }
    }
}