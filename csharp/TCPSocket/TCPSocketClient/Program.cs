using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 7486;            
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(host, port);
            }catch (SocketException e)
            {
                Console.WriteLine("Servidor não encontrado.");
                Console.ReadKey();
                return;
            }
            NetworkStream stream = client.GetStream();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            while (true)
            {
                Console.WriteLine("Insira sua mensagem para o papai noel:");
                string requestStr = Console.ReadLine();                
                sw.Write(requestStr);
                sw.Flush();
                string responseStr = sr.ReadToEnd();
                Console.WriteLine(responseStr);
                Console.ReadKey();
            }
        }
    }
}
