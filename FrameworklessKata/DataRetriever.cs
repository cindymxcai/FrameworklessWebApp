using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessKata
{
    public class DataRetriever
    {
        public List<Task> GetAllTasks()
        {
            var streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Tasks.json"));
            var jsonString= streamReader.ReadToEnd();
           
            streamReader.Close();

            return JsonConvert.DeserializeObject<List<Task>>(jsonString);
        }

        public void AddTask(Task task)
        {
            var allTasks = GetAllTasks();
            allTasks.Add(task);
            
            var newTasks = allTasks.Select(t => new JObject(new JProperty("Name", t.Name), new JProperty("Id", t.Id), new JProperty("isComplete", t.IsComplete)));
            var newJson = new JArray(newTasks);
            var streamWriter = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "Tasks.json"));
            streamWriter.WriteLine(newJson);
            
            streamWriter.Close();
        }

        public void UpdateTask(Task task)
        {
            
        }
        
        public void DeleteTask(Task task)
        {
        }
    }
}