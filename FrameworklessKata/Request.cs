using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace FrameworklessKata
{
    public class Request
    {
        private readonly Tasks _tasks;

        public Request(Tasks tasks)
        {
            _tasks = tasks;
        }
        public void Process(HttpListenerContext context)
        {
            try
            {
                HandleByMethod(context);
            } 
            catch (Exception e)
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                Response.Write("500", context);
                var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
        }

        private void HandleByMethod(HttpListenerContext context)
        {
           switch (context.Request.Url.AbsolutePath)
            {
                case "/":
                    context.Response.StatusCode = (int) HttpStatusCode.OK;
                    Response.Write("Index",context);
                    break;
                case "/tasks":
                    switch (context.Request.HttpMethod)
                    {
                        case "GET":
                            Console.WriteLine("getting tasks");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                        case "POST":
                            Console.WriteLine("posting to tasks");
                            context.Response.StatusCode = (int) HttpStatusCode.Created;
                            _tasks.Post(context);
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                        case "PUT":
                            Console.WriteLine("updating task");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            _tasks.Put(context);
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                        case "DELETE":
                            Console.WriteLine("deleting task");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            _tasks.Delete(context);
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                    }
                    break;
                default:
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    Response.Write("404", context);   
                    break;
            }
        }
    }
}