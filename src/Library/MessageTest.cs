using System.Threading.Tasks;

namespace Proyect
{

    /// <summary>
    /// Calse mensaje utilizada para los test.
    /// </summary>
    public class MessageTest : IMessage
    {
        private string id;
        private string text;
        private long msgid;

        /// <summary>
        /// Constructor de la clase message para el text.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="MSGID"></param>
        public MessageTest(string ID,int MSGID)
        {
            this.id = ID;
            this.msgid = MSGID;
        }

        /// <summary>
        /// Propierty get y set del id.
        /// </summary>
        /// <value>El id del usurio.</value>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Propierty del chat id de cada usaurio.
        /// </summary>
        /// <value>El chat id del usurio.</value>
        public long MsgId
        {
            get { return this.msgid; }
            set { this.msgid = value; }
        }

        /// <summary>
        /// El texto que envia el usaurio.
        /// </summary>
        /// <value>El texto.</value>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        /// <summary>
        /// Metodo que envia una imagen a un usuiro.
        /// </summary>
        /// <param name="mensaje">El mensaje que se mostrara con la imagen.</param>
        /// <param name="direccion">La dirrecion de donde se sacara la imagen.</param>
        /// <returns></returns>
        public Task SendProfileImage(string mensaje, string direccion)
        {
            return null;
        }
    }
}