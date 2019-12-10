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
            // Ip & Port = Connection
            int port = 18100;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ip, port);

            // Listening on port 18100
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start();

            // Title
            Console.WriteLine("Online Users");

            // List of Users
            List<string> Users = new List<string>();

            while (true)
            {
                // Listening on port 18100 and if there is any variation then it will detect it.
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[256];
                int read;

                // While loop that cycles thru every byte
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Online Users");
                    string message = Encoding.UTF8.GetString(buffer, 0, read);
                    Users.Add(message);
                    foreach (var item in Users)
                    {
                        Console.WriteLine(item);
                    }
                }
            }


        }
    }
}
