using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Server
    {
        private Boolean running;
        private TcpListener listener;

        public List<Thread> ClientThreads { get; set; }

        public List<String> Clients { get; set; }
        public List<Message> Messages { get; set; }
        public Int32 Port { get; set; }

        public Server(int port = 4321)
        {
            this.Port = port;
            Clients = new List<string>();
            Messages = new List<Message>();
            ClientThreads = new List<Thread>();
        }

        public void start()
        {
            running = true;
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            while (running)
            {
                Socket client = listener.AcceptSocket();
                Console.WriteLine("Connected: " + client.RemoteEndPoint.ToString());
                Thread t = new Thread(()=> serve(client));
                ClientThreads.Add(t);
                t.Start();
            }
            listener.Stop();
            listener = null;
        }

        public void stop()
        {
            running = false;
            ClientThreads.ForEach(c => c.Abort());            
        }

        public void serve(Socket socket)
        {
            string clientName = null;
            while (running)
            {
                string response = null;                
                byte[] clientData = new byte[1024];
                int size = socket.Receive(clientData);
                string strMessage = Encoding.ASCII.GetString(clientData);            
                string[] msg = strMessage.Substring(0, size).Split(';');
                if (msg.Length != 2)
                {
                    break;
                }
                if (msg[0] == "reg")
                {
                    clientName = msg[1];
                    Clients.Add(clientName);
                    response = "1";
                    Console.WriteLine("Register " + clientName);
                }
                else if (msg[0] == "msg")
                {
                    Messages.Add(new Message(clientName, msg[1], DateTime.Now));
                    response = "1";
                    Console.WriteLine("message from " + clientName + "::" + msg[1]);
                }
                else if (msg[0] == "lst")
                {
                    response = generateMessageList();
                    Console.WriteLine("List messages from " + clientName);
                }
                else
                {
                    response = "0";
                    Console.WriteLine("Request error of " + clientName);
                }
                byte[] responseData = Encoding.ASCII.GetBytes(response+"\n");
                socket.Send(responseData, SocketFlags.Partial);
            }            
            Console.WriteLine("Disconnected: " + socket.RemoteEndPoint.ToString());
            socket.Disconnect(false);
            
        }

        private string generateMessageList()
        {
            StringBuilder sb = new StringBuilder();
            Messages.ForEach(m =>
            {
                sb.Append(m.Datetime);
                sb.Append("|");
                sb.Append(m.Client);
                sb.Append("|");
                sb.Append(m.Text);
                sb.Append("#");
            });
            return sb.ToString();
        }
    }
}
