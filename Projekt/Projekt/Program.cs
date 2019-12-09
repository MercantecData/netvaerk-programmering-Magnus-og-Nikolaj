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
            int port = 18100;
            TcpClient client = new TcpClient();

            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();

            string text = "Test";

            byte[] buffer = Encoding.UTF8.GetBytes(text);

            stream.Write(buffer, 0, buffer.Length);

            client.Close();


            // Console.WriteLine("Hello World!");
            // var text = Console.ReadLine();
            // text = "æøå";
            // Console.WriteLine(Encoding.ASCII.GetBytes(text));
            // Console.WriteLine(Encoding.UTF8.GetBytes(text));

        }
    }
}
