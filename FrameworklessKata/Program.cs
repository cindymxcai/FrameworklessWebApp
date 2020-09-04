using System;

namespace FrameworklessKata
{
    internal static class Program
    {
        public static void Main(string[] args)
       {
            Server.Start();
            Console.WriteLine("Server not running!");
       }
    }
}