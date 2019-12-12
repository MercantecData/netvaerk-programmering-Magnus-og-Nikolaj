using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
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
            while (true)
            {
                // Console.Clear();
                Console.WriteLine("Connected");
                AwaitMessage(stream);
                Console.Write("Message: ");
                string message = "";
                message = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }

        }
        public static async void AwaitMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            while (true)
            {
                int numberOfBytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytes);
                Console.WriteLine(message);
            }
        }

    }
}
