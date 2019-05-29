using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace Telnyx.SharpNyx
{
    public class Message
    {
        //Message API Variables
        [JsonProperty("from")]
        public string FromPhoneNumber { get; set; } // Not required when pooling is turned on.
        [JsonProperty("to")]
        public string ToPhoneNumber { get; set; }
        [JsonProperty("delivery_status_webhook_url")]
        public string DeliveryStatusWebhookUrl { get; set; }
        [JsonProperty("delivery_status_failover_url")]
        public string DeliveryStatusFailoverUrl { get; set; }
        [JsonProperty("check_sender_health")]
        public bool CheckSenderHealth { get; set; }

        //Called by MAC when message is successfully queued
        [JsonIgnore]
        public bool IsQueued = false;

        public Message()
        {
        }

        public Message(string Source, string Destination)
        {
            this.FromPhoneNumber = Source;
            this.ToPhoneNumber = Destination;
        }
    }

    public class SMS : Message
    {
        [JsonProperty("body")]
        public string Body { get; set; } // SMS only field

        public SMS() { }

        public SMS(string Source, string Destination, string Body)
        {
            this.FromPhoneNumber = Source;
            this.ToPhoneNumber = Destination;
            this.Body = Body;
        }
    }

    public class MMS : Message
    {
        [JsonProperty("media_urls")]
        public IEnumerable<IDictionary<string, string>> MediaUrls { get; set; }

        public MMS() { }

        public MMS(string Source, string Destination, Dictionary<string, string> MediaUrls)
        {
            this.FromPhoneNumber = Source;
            this.ToPhoneNumber = Destination;
            this.MediaUrls = new List<Dictionary<string, string>>() { MediaUrls };
        }
    }

    public static class MediaUtype
    {
        public const string Image = "img";
        public const string Audio = "audio";
        public const string Video = "video";
        public const string Text = "text";
    }
}
