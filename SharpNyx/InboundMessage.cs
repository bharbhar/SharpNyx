using System.Collections.Generic;
using Newtonsoft.Json;
namespace SharpNyx
{
    public class InboundMessage
    {
        [JsonProperty("sms_id")]
        public string SMSId { get; set; }
        [JsonProperty("from")]
        public string Source { get; set; }
        [JsonProperty("to")]
        public string Destination { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("media")]
        public IEnumerable<InboundMedia> Media { get; set; }

        public static InboundMessage FromJson(string json)
        {
            return JsonConvert.DeserializeObject<InboundMessage>(json);
        }
    }

    public class InboundMedia
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
        [JsonProperty("hash_sha256")]
        public string Hash { get; set; }
        [JsonProperty("size")] //Bytes
        public int Size { get; set; }
    }
}
