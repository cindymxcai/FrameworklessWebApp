using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace FrameworklessKata
{
    public class Tasks  
    {
        private readonly DataRetriever _dataRetriever;

        public Tasks(DataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }
        public List<Task> Get()
        {
            return _dataRetriever.GetAllTasks();
        }

        private static Task GetTask( HttpListenerContext context)
        {
            var body = context.Request.InputStream;
            var streamReader = new StreamReader(body, context.Request.ContentEncoding);
            var json = streamReader.ReadToEnd();

            return JsonConvert.DeserializeObject<Task>(json);
        }

        public void Post(HttpListenerContext context)
        {
            var task = GetTask(context);
            _dataRetriever.AddTask(task);
        }

        public void Put(HttpListenerContext context)
        {
            var task = GetTask(context);
            _dataRetriever.UpdateTask(task);
        }

        public void Delete(HttpListenerContext context)
        {
            var task = GetTask(context);
            _dataRetriever.DeleteTask(task);
        }
    }
}