using System;
using System.Net;

namespace FrameworklessKata
{
    public class Server
    {
        private const int Port = 8080;

        private static readonly HttpListener Listener = new HttpListener();

        public static void Start()
        {
            Listener.Prefixes.Add($"http://localhost:{Port}/"); //URI prefixes 
            Listener.Start();
            Console.WriteLine("Starting Server... listening for requests");
            while (true)
            {
                var context = Listener.GetContext();
                Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                Request.Process(context);
            }

           // Listener.Stop(); // never reached...
        }
    }
}