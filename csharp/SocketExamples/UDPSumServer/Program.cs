using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPSumServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 60106;
            UdpClient srv = new UdpClient(port);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, port);
            while (true)
            {
                Console.WriteLine("Waiting for problems");
                byte[] request = srv.Receive(ref sender);
                string problem = Encoding.ASCII.GetString(request, 0, request.Length);
                Console.WriteLine("Received problem from {0} :\n {1}\n", sender.ToString(),problem);
                string[] numbers = problem.Split(' ');

                int n1 = int.Parse(numbers[0]);
                int n2 = int.Parse(numbers[1]);
                int sum = n1 + n2;
                byte[] response = Encoding.ASCII.GetBytes(sum.ToString());
                srv.Send(response, response.Length, sender);
            }
        }
    }
}
