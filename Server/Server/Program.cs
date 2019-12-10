using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 22010;
            IPAddress ip = IPAddress.Any;
            
            TcpListener listener = new TcpListener(ip, port);

            listener.Start();

            Console.WriteLine("Server started");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            GetMessage(stream);

            Console.WriteLine("Write message:");
            string userText = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(userText);

            stream.Write(buffer, 0, buffer.Length);

            Console.ReadKey();

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
