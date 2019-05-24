using Newtonsoft.Json;
namespace Telnyx.SharpNyx
{
    public class TelnyxMessage
    {
        [JsonProperty("src")]
        public string Source { get; set;}
        [JsonProperty("dst")]
        public string Destination { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
