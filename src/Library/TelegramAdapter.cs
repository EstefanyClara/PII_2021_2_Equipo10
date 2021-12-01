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
    /// Esto permite que la aplicacion sirva para otras apps apartes de telegram.
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
        /// <param name="message">El mensaje, en este caso es de telegram.</param>
        /// <param name="bot">El bot, en este caso de telegram.</param>
        public TelegramAdapter(Message message, TelegramBotClient bot)
        {
            this.message = message;
            this.id = message.From.Id;
            this.msgId = message.Chat.Id;
            this.bot = bot;
        }

        /// <summary>
        /// El mensaje ingresado.
        /// </summary>
        /// <value>El texto del mensaje.</value>
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
        /// El id de usurio.
        /// </summary>
        /// <value>El id de usuario.</value>
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
        /// <value>El id del chat de telegram.</value>
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
        /// Metodo que se encarga de enviar una foto al chat de telegram (Su utiliza el patron adapter).
        /// </summary>
        /// <param name="mensaje">El mensaje que se ingresa para enviar con la foto.</param>
        /// <param name="direccion">La dirrecion de donde se sacara la foto.</param>
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
