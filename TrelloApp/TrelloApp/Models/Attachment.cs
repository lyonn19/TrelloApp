using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApp.Models
{


    public class Attachment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "bytes")]
        public string Bytes { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "edgeColor")]
        public string EdgeColor { get; set; }

        [JsonProperty(PropertyName = "idMember")]
        public string IdMember { get; set; }

        [JsonProperty(PropertyName = "isUpload")]
        public bool IsUpload { get; set; }

        [JsonProperty(PropertyName = "mimeType")]
        public string MimeType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "previews")]
        public List<object> Previews { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "pos")]
        public int Pos { get; set; }
    }
}
