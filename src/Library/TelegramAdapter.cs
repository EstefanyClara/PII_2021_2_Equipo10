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
    /// Adaptador de mensajes de Telegram a mensajes de la interfaz de usuario.
    /// </summary>
    public class TelegramAdapter : IMessage
    {
        private Message message;
        private int id;
        private long msgId;

        private TelegramBotClient bot;

        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="TelegramAdapter"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="bot"></param>
        public TelegramAdapter(Message message, TelegramBotClient bot)
        {
            this.message = message;
            this.id = message.From.Id;
            this.msgId = message.Chat.Id;
            this.bot = bot;
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
            set
            {
                this.Text = value;
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
            set
            {
                this.id = int.Parse(value);
            }
        }
        
        /// <summary>
        /// Identificador del chat.
        /// </summary>
        /// <value></value>
        public long MsgId
        {
            get
            {
                return this.msgId;
            }
            set
            {
                this.msgId = value;
            }
        }

        /// <summary>
        /// Metodo que se encarga de neviar una foto al chat de telegram (Su utiliza el patron adapter).
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="direccion"></param>
        /// <returns></returns>
        public async Task SendProfileImage(string mensaje, string direccion)
        {
            if (bot != null)
            {
                await bot.SendChatActionAsync(this.MsgId, ChatAction.UploadPhoto);

                string filePath = direccion;
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

                await bot.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputOnlineFile(fileStream, fileName),
                    caption: mensaje
                );
            }
    }
    }
}
