using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Server socketServer = new Server("http://localhost:9050/");
            socketServer.Start();
            Console.ReadKey();
        }
    }
}
