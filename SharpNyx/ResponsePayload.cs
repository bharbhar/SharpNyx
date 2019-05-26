using System;
using Newtonsoft.Json;
namespace Telnyx.SharpNyx
{
    public class ResponsePayload
    {
        [JsonProperty("sms_id")]
        public string SMSId { get; set; }
        [JsonProperty("gw_sms_id")]
        public string GWSMSId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; }
        [JsonProperty("msg")]
        public OutgoingMessageReturn Message { get; set; }
        [JsonProperty("Coding")]
        public int Coding { get; set; }
        [JsonProperty("parts")]
        public int Parts { get; set; }
        [JsonProperty("created")]
        public double Created { get; set; }
        [JsonProperty("updated")]
        public double Updated { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }
        [JsonProperty("delivery_status_webhook_url")]
        public string DeliveryStatusWebhookUrl { get; set; }
        [JsonProperty("delivery_status_failover_url")]
        public string DeliveryStatusFailoverUrl { get; set; }

        public static ResponsePayload FromJson(string jSon)
        {
            return JsonConvert.DeserializeObject<ResponsePayload>(jSon);
        }
    }

    public class OutgoingMessageReturn
    {
        [JsonProperty("src")]
        public string Source { get; set; }
        [JsonProperty("dst")]
        public string Destination { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
