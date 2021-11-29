using System;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility.
    /// </summary>
    public class StartHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AutorizationHandler"/>. Esta clase procesa si la perosna que envio el mensaje esta registrada.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public StartHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/start"};
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
                response = $"¡Hola! C4Bot es un ChatBot dedicado a conectar organizaciones o empresas que ofertan ciertos productos, con emprendedores que necesitan o hacen uso de esos productos.\n\nLa logica de este bot esta orientada en un sitema de historia de usuario en donde a partir de un comando ingresado se puede hacer nada mas que de ciertos comandos permitidos en el contexto en el que se encuentre.\n\nDentro de las funcionalidades actuales estan las de Registrarse con /Registrar donde decidira que tipo de usuario sera, dependiendo de si cumple ciertos requerimientos.\n\nFuncionalidades para las organizaciones o companias:\nCon /Public podra publicar una oferta, siguiendo los pasos correspondientes.\nCon /MisOfertas podra ver todas su ofertas publicadas, y gestionar las mismas modificandolas o eliminandolas.\nCon /OfertasAceptadas podra ver todas las ofertas que publico que fueron aceptadas por emprendedores.\nCon /MaterialesConstantes podra obtener informacion acerca de cuantas ofertas constantes tiene cierto tipo de material.\n\n\nFuncionalidades para los emprendedores:\nCon /MaterialesConstantes podra obtener informacion acerca de cuantas ofertas constantes tiene cierto tipo de material para asi poder regular sus insumos.\nCon /OfertasAceptadas podra ver una lista de ofertas que acepto con la infromacion de compra.\n\nCon /Cancel cualquier usuario podra cancelar su operacion actual, así y volver al principio.";
                return true;
            }
            response = string.Empty;
            return false;
        }
    }
}