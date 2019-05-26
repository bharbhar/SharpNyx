using System.Collections.Generic;
namespace Telnyx.SharpNyx
{
    public class Message
    {
        //Message Variables
        public string FromPhoneNumber { get; set; }
        public string ToPhoneNumber { get; set; }
        public string Body { get; set; }
        public string DeliveryStatusWebhookUrl { get; set; }
        public string DeliveryStatusFailoverUrl { get; set; }
        public bool CheckSenderHealth { get; set; }

        public Message()
        {
        }

        public Message(string Source, string Destination, string Body)
        {
            this.FromPhoneNumber = Source;
            this.ToPhoneNumber = Destination;
            this.Body = Body;
        }

        public Dictionary<string, string> MessageClientDictionary()
        {
            //Add values to a dictionary for pipelineing into the FormUrlEncodedContent
            Dictionary<string, string> values = new Dictionary<string, string>
            {
               { "to", this.ToPhoneNumber },
               { "body", this.Body }
            };

            //This field is optional if Number Pool feature is enabled.
            if (this.FromPhoneNumber != null) values.Add("from", this.FromPhoneNumber);

            //Add delivery_status_webhook_url to the values if it is specified
            if (this.DeliveryStatusWebhookUrl != null) values.Add("delivery_status_webhook_url", this.DeliveryStatusWebhookUrl);

            //Add delivery_status_failover_url to the values if it is specified
            if (this.DeliveryStatusFailoverUrl != null) values.Add("delivery_status_failover_url", this.DeliveryStatusFailoverUrl);

            //Add check_sender_health to the values if it is specified
            if (this.CheckSenderHealth) values.Add("check_sender_health", "true");

            return values;
        }
    }
}
