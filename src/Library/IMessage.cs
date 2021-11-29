using System;

using Telegram.Bot.Types;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

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

        /// <summary>
        /// Id del chat.
        /// </summary>
        /// <value></value>
        long MsgId{get;}

        /// <summary>
        /// Envia una imagen a un usuario
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="direccion"></param>
        /// <returns></returns>
        Task SendProfileImage(string mensaje, string direccion);
    }
}