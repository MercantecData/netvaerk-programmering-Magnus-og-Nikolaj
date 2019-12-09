using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            // Ip & Port = Connection
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 18100;
            IPEndPoint endPoint = new IPEndPoint(ip, port);


            // When Connected get a tcp stream running
            TcpClient client = new TcpClient();
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();


            // Sending your username to the server
            string username = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(username);
            stream.Write(buffer, 0, buffer.Length);


            // Closing the tcp stream
            client.Close();
        }
    }
}
