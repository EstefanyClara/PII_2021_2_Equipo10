using System;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class AutorizationHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AutorizationHandler"/>. Esta clase procesa si la perosna que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public AutorizationHandler(BaseHandler next) : base(next)
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
            if (!this.Keywords.Contains(message.Text.ToLower().Replace(" ","")) & !DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
            {
                if (AppLogic.Instance.GetCompany(message.Id) != null | AppLogic.Instance.GetEmprendedor(message.Id) != null)
                {
                    response = string.Empty;
                    return false;
                }
                else
                {
                                        response = "Antes de interactuar con la aplicacion debe registrarse";
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}