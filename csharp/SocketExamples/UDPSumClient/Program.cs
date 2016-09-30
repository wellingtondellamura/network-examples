using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPSumClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 60106;
            IPAddress address = IPAddress.Parse("127.0.0.1");

            Console.WriteLine("number 1: ");
            string n1 = Console.ReadLine();
            Console.WriteLine("number 2: ");
            string n2 = Console.ReadLine();
            string problem = n1 + " " + n2;
            byte[] sendbuf = Encoding.ASCII.GetBytes(problem);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);            
            IPEndPoint ep = new IPEndPoint(address, port);
            s.SendTo(sendbuf, ep);
            Console.WriteLine("Problem sent to the server");

            byte[] receivebuf = new byte[100];
            s.Receive(receivebuf);

            string receive = Encoding.ASCII.GetString(receivebuf);
            Console.WriteLine("Result:");
            Console.WriteLine(receive.Trim());
            Console.ReadKey();
        }

    }
}
