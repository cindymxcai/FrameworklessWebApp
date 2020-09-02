using System.Net;

namespace FrameworklessKata
{
    public static class Response
    {
        public static void Write(string message, HttpListenerContext context)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message); 
            context.Response.ContentLength64 = buffer.Length;  
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);  // forces send of response  
        }
    }
}