using System;

namespace Proyect
{
    /// <summary>
    /// Interfaz para los mensajes.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Id del usuario.
        /// </summary>
        string Id { get;}

        /// <summary>
        /// El mensaje.
        /// </summary>
        string Text { get;}
    }
}