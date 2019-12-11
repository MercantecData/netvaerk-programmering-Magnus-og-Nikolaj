using System;
using NetMQ;
using NetMQ.Sockets;
using Terminal.Gui;

namespace GUI_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            var top = Application.Top;

            // Window
            var win = new Window(new Rect(0, 1, top.Frame.Width, top.Frame.Height - 1), "EchoFive");
            top.Add(win);

            // Menubar.
            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
            new MenuItem ("_Connect", "", () => {}),
            new MenuItem ("_Quit", "", () => { top.Running = false; })
            })
            });
            top.Add(menu);
            var start = new Button(4, 1, "Start Server");
            win.Add(start);

            var counter = 4;

            start.Clicked = () =>
            {
                while (true)
                {

                using (var server = new ResponseSocket())
                {
                    server.Bind("tcp://*:18100");

                    string m1 = server.ReceiveFrameString();
                    win.Add(new Label(4, counter, m1));
                    counter += 2;
                    server.SendFrame("Server: Connected");

                    server.Disconnect("tcp://*:18100");
                    server.Close();
                }
                }
            };
            Application.Run();
        }
    }
}
