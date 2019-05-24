using Newtonsoft.Json;
namespace Telnyx.SharpNyx
{
    public class ResponseError
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("success")]
        public string Success { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public static ResponseError FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ResponseError>(json);
        }
    }
}
