using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        public static List<TcpClient> clients = new List<TcpClient>();
        public static List<string> log = new List<string>();
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 18100;
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            AcceptClients(listener);

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Write message: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                foreach (TcpClient client in clients)
                {
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }
            }
        }
        public static async void AcceptClients(TcpListener listener)
        {
            bool isRunning = true;
            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                AwaitMessage(stream);
            }
        }
        public static async void AwaitMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            bool isRunning = true;
            while (isRunning)
            {
                int numberOfBytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytes);
                log.Add(message);
                Console.WriteLine(message);
            }
        }
    }
}
