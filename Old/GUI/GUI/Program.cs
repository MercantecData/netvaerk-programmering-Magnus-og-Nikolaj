using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;
using Terminal.Gui;

namespace GUI
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

            // Functions for constrols
            var usernameText = new Label(4, 2, "Username: ");
            var username = new TextField(16, 2, 40, "");
            var ipaddressText = new Label(4, 4, "IPAddress: ");
            var ipaddress = new TextField(16, 4, 40, "127.0.0.1");
            var portText = new Label(4, 6, "Port: ");
            var port = new TextField(16, 6, 10, "18100");
            var login = new Button(4, 8, "Login");
            var cancel = new Button(13, 8, "Cancel");
            var status = new Label(3, 16, "Disconnected");
            var info = new Label(3, 18, "Press ESC and 9 to activate the menubar");

            login.Clicked = () =>
            {
                using (var client = new RequestSocket())
                {
                    string ip = Convert.ToString(ipaddress.Text);
                    string portT = Convert.ToString(port.Text);
                    client.Connect("tcp://" + ip + ":" + portT);
                    status.Text = "Connected";
                    string texting = Convert.ToString(username.Text);
                    client.SendFrame(texting);

                    string m2 = client.ReceiveFrameString();
                    status.Text = m2;
                    client.Disconnect("tcp://" + ip + ":" + portT);
                    client.Close(); 
                }
            };

            cancel.Clicked = () =>
            {
                username.Text = "";
                ipaddress.Text = "localhost";
                port.Text = "18100";
            };

            // Added some controls to the screen
            win.Add(
                usernameText,
                username,
                ipaddressText,
                ipaddress,
                portText,
                port,
                login,
                cancel,
                status,
                info);

            Application.Run();

        }
    }
}
