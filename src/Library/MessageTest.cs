using System.Threading.Tasks;

namespace Proyect
{
    public class MessageTest : IMessage
    {
        private string id;
        private string text;
        private long msgid;

        public string Id
        {
            get{return this.id;}
            set{this.id = value;}
        }

        public long MsgId
        {
            get{return this.msgid;}
            set{this.msgid = value;}
        }
        public string Text
        {
            get{return this.text;}
            set{this.text = value;}
        }

        public Task SendProfileImage(string mensaje, string direccion)
        {
            return null;
        }
    }
}   