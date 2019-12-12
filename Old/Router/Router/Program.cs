using System;
using System.Collections.Generic;
using System.Text;
using NetMQ;
using NetMQ.Sockets;

namespace Router
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new RouterSocket("@tcp://*:18100"))
            {
                var addresses = new HashSet<string>();
                var msg = new NetMQMessage();

                while (true)
                {
                    var clientHasMsg = server.TryReceiveMultipartMessage(TimeSpan.FromSeconds(1), ref msg);
                    if (!clientHasMsg)
                    {
                        var broadMsg = new NetMQMessage();
                        foreach (var item in addresses)
                        {
                            broadMsg.Append(item);
                            broadMsg.AppendEmptyFrame();
                            broadMsg.Append("This is a broadcast");
                            server.SendMultipartMessage(broadMsg);
                            broadMsg.Clear();
                        }
                        continue;
                    }

                    var address = Encoding.UTF8.GetString(msg[0].Buffer);
                    var payload = Encoding.UTF8.GetString(msg[2].Buffer);
                    Console.WriteLine("[Server] - Client: {0} Says: {1}", address, payload);

                    var contains = addresses.Contains(address);
                    if (!contains) { addresses.Add(address); }

                    msg.Clear();
                    msg.Append(address);
                    msg.AppendEmptyFrame();
                    msg.Append("Reply for: " + address);
                    server.SendMultipartMessage(msg);
                }
            }
        }
    }
}
