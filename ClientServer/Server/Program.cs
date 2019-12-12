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

            while (true)
            {
                continue;
            }
        }
        public static async void AcceptClients(TcpListener listener)
        {
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                string ip = Convert.ToString(client.Client.RemoteEndPoint);
                AwaitMessage(stream, ip);
                Console.WriteLine("Client Connected From: " + client.Client.RemoteEndPoint);
            }
        }
        public static async void AwaitMessage(NetworkStream stream, string ip)
        {
            byte[] buffer = new byte[256];
            while (true)
            {
                int numberOfBytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = ip + ": " + Encoding.UTF8.GetString(buffer, 0, numberOfBytes);
                log.Add(message);
                Console.WriteLine(message);

                string response = "";
                foreach (string l in log)
                {
                    response += l;
                    response += "\n";
                }

                byte[] bufferResponse = Encoding.UTF8.GetBytes(response);
                foreach (TcpClient client in clients)
                {
                    client.GetStream().Write(bufferResponse, 0, bufferResponse.Length);
                }
            }
        }
    }
}
