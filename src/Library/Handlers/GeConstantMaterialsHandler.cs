using System;
using System.Linq;
using System.Text;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class GetConstantMaterialsHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetConstantMaterialsHandler"/>. Esta clase procesa si la perosna que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public GetConstantMaterialsHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/materialesconstantes"};
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
                if( DataUserContainer.Instance.UserDataHistory.Keys.Contains(message.Id))
                {
                    response = "Para utilizar este comando primero debe terminar elproceso actual";
                    return true;
                }else
                {
                    StringBuilder mensaje = new StringBuilder();
                    mensaje.Append("Los materiales constantes presentes en nuestra aplicacion actualmen son:\n");
                    foreach(var item in AppLogic.Instance.GetConstantMaterials())
                    {
                        mensaje.Append($"\n{item.Key.Category}: {(item.Value > 0 ? item.Value.ToString() + " Oferta/s" : "Sin ofertas que lo ofrescan de forma constante.")}");
                    }
                    response = mensaje.ToString();
                }
                return true;
            }
            response = string.Empty;
            return false;
        }
    }
}