using System;
using Newtonsoft.Json;
namespace Telnyx.SharpNyx
{
    public class MasterDetailRecord
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
        [JsonProperty("created")]
        public double Created { get; set; }
        [JsonProperty("updated")]
        public double Updated { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }
        [JsonProperty("body")]
        public MDRMessageBody Body { get; set; }
        [JsonProperty("from")]
        public string Source { get; set; }
        [JsonProperty("to")]
        public string Destination { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
        [JsonProperty("on_net")]
        public bool OnNet { get; set; }
        [JsonProperty("cost")]
        public string Cost { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("carrier")]
        public string Carrier { get; set; }
        [JsonProperty("line_type")]
        public string LineType { get; set; }

        public static MasterDetailRecord FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MasterDetailRecord>(json);
        }
    }

    public class MDRMessageBody
    {
        [JsonProperty("coding")]
        public int Coding { get; set; }
        [JsonProperty("num_chars")]
        public int Characters { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("text_hash")]
        public string TextHash { get; set; }
        [JsonProperty("num_bytes")]
        public int Size { get; set; } //In Bytes
        [JsonProperty("bytes_hash")]
        public string ByteHash { get; set; }
        [JsonProperty("parts")]
        public int Parts { get; set; }
    }
}
