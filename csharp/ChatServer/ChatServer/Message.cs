using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Message
    {
        public DateTime Datetime { get; set; }
        public string Client { get; set; }
        public string Text { get; set; }

        public Message(string client, string text, DateTime datetime)
        {
            this.Client = client;
            this.Text = text;
            this.Datetime = datetime;
        }
    }
}
