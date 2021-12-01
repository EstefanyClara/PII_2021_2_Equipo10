using System;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// Hereda de base Hanlder.
    /// </summary>
    public class AdministratorHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AdministratorHandler"/>. Esta clase procesa el mensaje de un uasurio para tener el rol de administrador.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public AdministratorHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/nuevoadministrador", "/invitar" };
        }

        /// <summary>
        /// Procesa el mensaje "/nuevoadministrador" y  "/invitar", retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            string[] comando = message.Text.Trim(' ').ToLower().Split(" ");
            if (comando.Count() <= 2)
            {
                if (comando.Count() == 2 && comando[0].Equals("/nuevoadministrador"))
                {
                    if (AppLogic.Instance.AddAdministrator(message.Id, comando[1]))
                    {
                        response = "Usted tiene el rol de administrador";
                        return true;
                    }
                    else
                    {
                        response = "El codigo ingresado no es correcto";
                        return true;
                    }
                }
                if (comando.Count() == 1 && comando[0].Equals("/invitar") && Administrator.Instance.IsAdministrator(message.Id))
                {
                    response = AppLogic.Instance.Invite();
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}