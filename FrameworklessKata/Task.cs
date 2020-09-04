using Newtonsoft.Json;

namespace FrameworklessKata
{
    public class Task
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName ="id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool IsComplete { get; set; }
    }
}