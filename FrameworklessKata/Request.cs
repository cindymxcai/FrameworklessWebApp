using System;
using System.IO;
using System.Net;

namespace FrameworklessKata
{
    public static class Request
    {
        public static void Process(HttpListenerContext context)
        {
            switch (context.Request.Url.AbsolutePath)
            {
                case "/":
                    Response.Write("Index",context);
                    break;
                case "/ids":
                    switch (context.Request.HttpMethod)
                    {
                        case "GET":
                            Console.WriteLine("getting IDs");
                            Response.Write("Returned IDs", context);
                            break;
                        case "POST":
                            Console.WriteLine("posting to IDs");
                            var body = context.Request.InputStream;
                            var reader = new StreamReader(body, context.Request.ContentEncoding);
                            Response.Write(reader.ReadToEnd(), context);
                            break;
                    }
                    break;
                default:
                    Response.Write("404", context);   
                    break;
            }
        }
    }
}