using System;
using System.Net.Sockets;

namespace HTTPClient
{
    class Program
    {

        //https://www.ietf.org/rfc/rfc2616.txt
        static void Main(string[] args)
        {
            string host = "cct.uenp.edu.br";
            int port = 80;
            string requestStr = "";
            requestStr += "GET /wellington/test.html HTTP/1.0\r\n";
            requestStr += "Host: cct.uenp.edu.br\r\n";
            requestStr += "\r\n";

            Console.WriteLine("-== HTTP CLIENT ==-");
            TcpClient client = new TcpClient();
            Console.WriteLine("Trying to connect to " + host + ":" + port);
            client.Connect(host, port);
            if (client.Connected)
            {
                Console.WriteLine("Connection established");
            }
            else
            {
                Console.WriteLine("Connection failed");
                return;
            }
            NetworkStream stream = client.GetStream();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            Console.WriteLine("Sending request: \n" + requestStr);
            sw.Write(requestStr);
            sw.Flush();
            Console.WriteLine("Waiting response");
            string responseStr = sr.ReadToEnd();
            Console.WriteLine("------------------ Response ------------------ ");
            Console.WriteLine(responseStr);
            Console.WriteLine("-------------- End of Response --------------- ");
            Console.ReadKey();
        }
    }
}
