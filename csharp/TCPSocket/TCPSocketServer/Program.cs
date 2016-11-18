using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> msg = new List<string>();
            Boolean foo = true;
            TcpListener server = new TcpListener(IPAddress.Any, 7486);
            server.Start();
            while (foo)
            {
                Socket client = server.AcceptSocket();
                Thread t = new Thread(()=>{                    
                    byte[] clientData = new byte[1024];
                    int size = client.Receive(clientData);
                    string m = Encoding.ASCII.GetString(clientData);
                    //s;blablabla
                    //r
                    var x = m.Split(';');
                    if (x[0] == "s")
                    {
                        msg.Add(x[1]);
                        byte[] responseData = Encoding.ASCII.GetBytes("ok");
                        client.Send(responseData);
                    }
                    else if (x[0] == "r")
                    {
                        string todos = " ";
                        //msg.ForEach(e => todos += e);
                        foreach (string s in msg)
                        {
                            todos = todos+ "\n" + s;
                        }
                        byte[] responseData = Encoding.ASCII.GetBytes(todos);
                        client.Send(responseData);
                    } else
                    {
                        Console.WriteLine("Não to entendendo "+m);
                    }              
                });
                t.Start();
            }
            server.Stop();
            Console.ReadKey();
        }
    }
}
