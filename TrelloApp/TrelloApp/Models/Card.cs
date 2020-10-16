using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApp.Models
{
    public class Card
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Desc { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "badges")]
        public Badges Badges { get; set; }

    }


    public class Badges
    {
        [JsonProperty(PropertyName = "attachments")]
        public int Attachments { get; set; }
    }
}
