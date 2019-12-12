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
            Client();

            void Client()
            {
                int counter = 0;
                TcpClient client = new TcpClient();
                while (client.Connected == false)
                {

                    try
                    {
                        // client.Connect("172.16.116.211", 18100);
                        client.ConnectAsync("172.16.116.211", 18100);
                    }
                    catch
                    {
                        Console.Clear();
                        if (counter == 0)
                        {
                            Console.WriteLine("Waiting for the server... //");
                            counter = 1;
                        }
                        else if (counter == 1)
                        {
                            Console.WriteLine("Waiting for the server... --");
                            counter = 2;
                        }
                        else
                        {
                            Console.WriteLine("Waiting for the server... \\\\");
                            counter = 0;
                        }
                        Thread.Sleep(500);
                    }
                }
                NetworkStream stream = client.GetStream();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Connected");
                    AwaitMessage(stream);
                    Console.Write("Message: ");
                    string message = "";
                    message = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);
                }
                    // client.Close();
            }

            async void AwaitMessage(NetworkStream stream)
            {
                byte[] buffer = new byte[256];
                int numberOfBytes = await stream.ReadAsync(buffer, 0, 256);
                string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytes);
                Console.WriteLine(message);
            }
        }

    }
}
