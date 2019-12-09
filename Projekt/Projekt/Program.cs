using System;
using System.Text;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var text = Console.ReadLine();
            text = "æøå";
            Console.WriteLine(Encoding.ASCII.GetBytes(text));
            Console.WriteLine(Encoding.UTF8.GetBytes(text));

        }
    }
}
