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
    }
}
