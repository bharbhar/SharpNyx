using Newtonsoft.Json;
namespace Telnyx.SharpNyx
{
    public class RejectedResponsePayload
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public static RejectedResponsePayload FromJson(string json)
        {
            return JsonConvert.DeserializeObject<RejectedResponsePayload>(json);
        }
    }
}
