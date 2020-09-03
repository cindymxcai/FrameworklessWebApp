using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FrameworklessKata
{
    public class DataRetrieval
    {
        public List<Tasks> GetAllTasks()
        {
            var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Tasks.json"));
            var json= sr.ReadToEnd();
            sr.Close();

            return JsonConvert.DeserializeObject<List<Tasks>>(json);
            
        }
    }
}