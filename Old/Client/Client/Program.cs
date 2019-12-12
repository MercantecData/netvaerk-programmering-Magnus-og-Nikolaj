using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using EasyConsole;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            int port = 22010;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            client.Connect(ip, port);

            NetworkStream stream = client.GetStream();
            GetMessage(stream);

            Console.WriteLine("Write message");
            string text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);

            client.Close();
            static async void GetMessage(NetworkStream stream)
            {
                byte[] buffer = new byte[256];

                int numOfBytes = await stream.ReadAsync(buffer, 0, 256);
                string message = Encoding.UTF8.GetString(buffer, 0, numOfBytes);

                Console.WriteLine(message);
            }
        }
    }
}
