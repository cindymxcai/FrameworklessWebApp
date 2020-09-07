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
                    switch (ConvertHttpMethodToEnum(context.Request.HttpMethod))
                    {
                        case HttpMethod.Get:
                            Console.WriteLine("getting tasks");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                        case HttpMethod.Post:
                            Console.WriteLine("posting to tasks");
                            context.Response.StatusCode = (int) HttpStatusCode.Created;
                            _tasks.Post(context);
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                        case HttpMethod.Put:
                            Console.WriteLine("updating task");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            _tasks.Put(context);
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                        case HttpMethod.Delete:
                            Console.WriteLine("deleting tasks");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            _tasks.DeleteAll();
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                    }
                    break;
                case "/task":
                {
                    switch (ConvertHttpMethodToEnum(context.Request.HttpMethod))
                    {
                        case HttpMethod.Get:
                            Console.WriteLine("getting task");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            Response.Write($"Task:\n {JsonConvert.SerializeObject(_tasks.GetTask(context))}", context);
                            break;
                        case HttpMethod.Delete:
                            Console.WriteLine("deleting task");
                            context.Response.StatusCode = (int) HttpStatusCode.OK;
                            _tasks.Delete(context);
                            Response.Write($"Tasks:\n {JsonConvert.SerializeObject(_tasks.Get())}", context);
                            break;
                    }
                        
                    break;
                }
                default:
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    Response.Write("404", context);   
                    break;
            }
        }

        private HttpMethod ConvertHttpMethodToEnum(string requestHttpMethod)
        {
            if (Enum.TryParse(requestHttpMethod, true,  out HttpMethod method))
            {
                return method;
            }
            throw new Exception("Invalid method");
        }
    }
}