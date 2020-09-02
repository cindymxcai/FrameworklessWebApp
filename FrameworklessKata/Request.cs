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
                default:
                    Response.Write("404", context);   
                    break;
            }
        }
    }
}