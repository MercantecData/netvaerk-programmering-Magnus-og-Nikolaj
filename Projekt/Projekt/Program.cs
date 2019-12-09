using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 13556;





            Console.WriteLine("Hello World!");
            var text = Console.ReadLine();
            text = "æøå";
            Console.WriteLine(Encoding.ASCII.GetBytes(text));
            Console.WriteLine(Encoding.UTF8.GetBytes(text));

        }
    }
}
