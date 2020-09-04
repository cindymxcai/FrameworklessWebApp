using System;
using System.Net;

namespace FrameworklessKata
{
    public static class Server
    {
        private const int Port = 8080;

        private static readonly HttpListener Listener = new HttpListener();

        public static void Start()
        {
            var dataRetriever = new DataRetriever();
            var tasks = new Tasks(dataRetriever);
            var request = new Request(tasks);

            Listener.Prefixes.Add($"http://localhost:{Port}/"); 
            Listener.Start();
            Console.WriteLine("Starting Server... listening for requests");
            while (true)
            {
                var context = Listener.GetContext();
                Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                request.Process(context);
            }

           // Listener.Stop(); // never reached...
        }
    }
}