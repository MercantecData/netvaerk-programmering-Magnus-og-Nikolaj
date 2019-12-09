using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

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

            while (true)
            { 
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[256];
                int read = -1;

                // While loop that cycles thru every byte
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, read);
                    Console.WriteLine(message);
                }
            }


        }
    }
}
