using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean foo = true;
            TcpListener server = new TcpListener(IPAddress.Any, 7486);
            server.Start();
            while (foo)
            {
                Socket client = server.AcceptSocket();
                byte[] clientData = new byte[1024];
                int size = client.Receive(clientData);
                for (int i = 0; i < size; i++)
                {
                    Console.Write(Convert.ToChar(clientData[i]));
                }
                Console.WriteLine();
                byte[] responseData = Encoding.ASCII.GetBytes("Bye");
                client.Send(responseData);                
                client.Close();
            }
            server.Stop();
            Console.ReadKey();
        }
    }
}
