using Newtonsoft.Json;

namespace TrelloApp.Models
{
    public class Board
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "closed")]
        public bool Closed { get; set; }
        
        [JsonProperty(PropertyName = "pos")]
        public int Pos { get; set; }
        
        [JsonProperty(PropertyName = "softLimit")]
        public object SoftLimit { get; set; }
        
        [JsonProperty(PropertyName = "idBoard")]
        public string IdBoard { get; set; }
        
        [JsonProperty(PropertyName = "subscribed")]
        public bool Subscribed { get; set; }

        public int CardsCount { get; set; }
    }
}
