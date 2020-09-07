using System;
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

        public string ReadBody( HttpListenerContext context)
        {
            var body = context.Request.InputStream;
            var streamReader = new StreamReader(body, context.Request.ContentEncoding);
            return  streamReader.ReadToEnd();

        }

        public void Post(HttpListenerContext context)
        {
            var json = ReadBody(context);
            var task = JsonConvert.DeserializeObject<Task>(json);
            _dataRetriever.AddTask(task);
        }

        public void Put(HttpListenerContext context)
        {
            var json = ReadBody(context);
            var task = JsonConvert.DeserializeObject<Task>(json);
            _dataRetriever.UpdateTask(task);
        }

        public void Delete(HttpListenerContext context)
        {
            var json = ReadBody(context);

            if (int.TryParse(json, out var taskId)) _dataRetriever.DeleteTask(taskId);

        }

        public Task GetTask(HttpListenerContext context)
        {
            var json = ReadBody(context);
            if (int.TryParse(json, out var taskId)) return _dataRetriever.GetTask(taskId);
            throw new Exception("No Task with this Id");
        }

        public void DeleteAll()
        {
            _dataRetriever.DeleteAllTasks();
        }
    }
}