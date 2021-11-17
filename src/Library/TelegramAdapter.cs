using Telegram.Bot.Types;
using System.Linq;

namespace Proyect
{
    /// <summary>
    /// Adaptador de mensajes de Telegram a mensajes de la interfaz de usuario.
    /// </summary>
    public class TelegramAdapter : IMessage
    {
        private Message message;
        private int id;
        private int msgId;

        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="TelegramAdapter"/>.
        /// </summary>
        /// <param name="message"></param>
        public TelegramAdapter(Message message)
        {
            this.message = message;
            this.id = message.From.Id;
        }

        /// <summary>
        /// El mensaje recibido.
        /// </summary>
        public string Text
        {
            get
            {
                return this.message.Text;
            }
        }

        /// <summary>
        /// Identificador del usuario que envió el mensaje.
        /// </summary>
        public string Id
        {
            get
            {
                return this.id.ToString();
            }
        }
        
        /// <summary>
        /// Identificador del chat.
        /// </summary>
        /// <value></value>
        public string MsgId
        {
            get
            {
                return this.msgId.ToString();
            }
        }
    }
}