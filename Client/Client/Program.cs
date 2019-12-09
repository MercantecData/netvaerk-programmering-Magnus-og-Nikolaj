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

            void Connect()
            {
                Console.Clear();
                string input = Input.ReadString("Please enter an IP Address:");
                Output.WriteLine("Connecting to: {0}", input);

                try
                {
                    // Ip & Port = Connection
                    IPAddress ip = IPAddress.Parse(input);
                    int port = 18100;
                    IPEndPoint endPoint = new IPEndPoint(ip, port);

                    // When Connected get a tcp stream running
                    TcpClient client = new TcpClient();
                    client.Connect(endPoint);
                    NetworkStream stream = client.GetStream();

                    // Sending your username to the server
                    // string username = Console.ReadLine();
                    string username = Input.ReadString("Username:");
                    byte[] buffer = Encoding.UTF8.GetBytes(username);
                    stream.Write(buffer, 0, buffer.Length);

                    // Closing the tcp stream
                    client.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to connect to the server");
                    Console.ReadLine();
                }

                Menu();

            }

            void Menu()
            {
                Console.Clear();
                var menu = new EasyConsole.Menu();
                menu.Add("Connect", () => Connect());
                menu.Add("Online Users", () => Console.WriteLine("Test"));
                menu.Display();
            }

            Menu();

        }

    }
}
