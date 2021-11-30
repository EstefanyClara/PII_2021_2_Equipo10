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
    public class MessageTest : IMessage
    {
        private string id;
        private string text;
        private long msgid;

        public MessageTest(string ID,int MSGID)
        {
            this.id = ID;
            this.msgid = MSGID;
        }

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