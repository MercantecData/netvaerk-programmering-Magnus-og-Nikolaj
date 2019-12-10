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
            // IPEndPoint localEndPoint = new IPEndPoint(ip, port);

            // Listening on port 18100
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            // Title
            Console.WriteLine("Online Users");

            // List of Users
            List<string> Users = new List<string>();

            while (true)
            {
                // Listening on port 18100 and if there is any variation then it will detect it.
                byte[] buffer = new byte[256];
                int read = -1;

                // While loop that cycles thru every byte
                while ((read = listener.AcceptTcpClient().GetStream().Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, read);
                    if (message == "command:onlineusers")
                    {
                        Console.WriteLine("Sending all users");
                        byte[] bufferResponse = Encoding.UTF8.GetBytes("Response");
                        listener.AcceptTcpClient().GetStream().Write(bufferResponse, 0, bufferResponse.Length);

                    } else
                    {
                        Console.Clear();
                        Console.WriteLine("Online Users");
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
}
