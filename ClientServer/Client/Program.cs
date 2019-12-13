using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        private static string username = "";
        static void Main(string[] args)
        {
            int counter = 0;
            TcpClient client = new TcpClient();
            while (client.Connected == false)
            {
                try { client.ConnectAsync("127.0.0.1", 18100); }
                catch
                {
                    Console.Clear();
                    switch (counter)
                    {
                        case 0:
                            Console.WriteLine("Waiting for the server... //");
                            counter = 1;
                            break;
                        case 1:
                            Console.WriteLine("Waiting for the server... --");
                            counter = 2;
                            break;
                        case 2:
                            Console.WriteLine("Waiting for the server... \\\\");
                            counter = 0;
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(500);
                }
            }
            NetworkStream stream = client.GetStream();
            int count = 0;
            while (true)
            {
                Console.WriteLine("Connected");
                AwaitMessage(stream);
                string message;

                if (count == 0) // First loop
                {
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    message = username;
                    count++;
                }
                else
                {
                    message = Console.ReadLine();
                }
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
                // Console.Clear();
                
            }

        }
        public static async void AwaitMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                int numberOfBytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytes);
                Console.Clear();
                Console.WriteLine(message);
                Console.Write("Write msg as "+username+": ");
            }
        }

    }
}
