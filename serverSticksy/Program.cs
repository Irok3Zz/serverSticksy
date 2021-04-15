using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace serverSticksy
{
    class Program
    {
        private static string ip = "127.0.0.1";
        private static int port = 8081;
        private static Server server;

        static void Main(string[] args)
        {
            server = new Server(new IPEndPoint(IPAddress.Parse(ip), port));
            server.Listen();

            Console.WriteLine("Server stopped");
            Console.ReadKey();
        }
    }
}
