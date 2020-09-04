using System;
using System.Net;

namespace FrameworklessKata
{
    public static class Response
    {
        public static void Write(string message, HttpListenerContext context)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello, the time on the server is {DateTime.Now}\n{message}");
            context.Response.ContentLength64 = buffer.Length;  //gets number of bytes in body 
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);  // forces send of response  
            context.Response.Close();
        }
    }
}