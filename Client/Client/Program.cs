using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using EasyConsole;

namespace Client
{
    class Program
    {
        public static Int32 port = 18100;
        public static string ip = "127.0.0.1";

        static void Main(string[] args)
        {

            void Connect()
            {
                Console.Clear();
                string IpInput = Input.ReadString("Please enter an IP Address: (127.0.0.1)");
                if (IpInput == "")
                    IpInput = "127.0.0.1";

                Output.WriteLine("Connecting to: {0}", IpInput);
                int PortInput = Input.ReadInt("Please enter a PORT: (18100)", min: 0, max: 65535);
                if (PortInput == 0)
                    PortInput = 18100;

                try
                {
                    // Ip & Port = Connection
                    ip = IpInput;
                    port = PortInput;

                    // When Connected get a tcp stream running
                    TcpClient client = new TcpClient(ip, port);

                    // Sending your username to the server
                    Output.WriteLine(ConsoleColor.Green, "Connected");
                    string username = Input.ReadString("Username:");
                    byte[] buffer = Encoding.UTF8.GetBytes(username);
                    client.GetStream().Write(buffer, 0, buffer.Length);
                    
                    // Closing the tcp stream
                    client.GetStream().Close();
                    client.Close();
                }
                catch (Exception)
                {
                    Output.WriteLine(ConsoleColor.Red, "Failed to connect to the server");
                    Console.ReadLine();
                }

                Menu();

            }

            void Users() // command:onlineusers
            {
                // Client Sending a command
                TcpClient client = new TcpClient(ip, port);
                byte[] buffer = Encoding.UTF8.GetBytes("command:onlineusers");
                client.GetStream().Write(buffer, 0, buffer.Length);

                Console.Clear();
                Output.WriteLine(ConsoleColor.Green, "Online Users");

                byte[] bufferResponse = new byte[256];
                int read = -1;
                if ((read = client.GetStream().Read(bufferResponse, 0, bufferResponse.Length)) > 0 )
                {
                    string message2 = Encoding.UTF8.GetString(bufferResponse, 0, read);
                    Console.WriteLine(message2);
                }
                client.GetStream().Close();
                client.Close();



                Console.ReadLine();
                Menu();

            }

            void Menu()
            {
                Console.Clear();
                var menu = new EasyConsole.Menu();
                menu.Add("Connect", () => Connect());
                menu.Add("Online Users", () => Users());
                menu.Add("Exit", () => Output.WriteLine(ConsoleColor.Red, "Exiting..."));
                menu.Display();
            }

            Menu();
        }
    }
}
