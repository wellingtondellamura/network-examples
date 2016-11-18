using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Server s = new Server();            
            s.start();
            //Console.WriteLine("Press any key to terminate");
            //Console.ReadKey();
            //s.stop();
        }
    }
}
