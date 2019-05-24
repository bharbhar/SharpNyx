//2019 Bharat Bhardwaj Bugs Inc. of California
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Telnyx.SharpNyx
{
    public class TelnyxRestClient
    {
        //It is recommended to instantiate one HttpClient for your application's lifetime and share it.
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Account XProfileSecret found in Message Profiles
        /// </summary>
        public string XProfileSecret { get; set; }

        //Response is null untill SendSMSAsync is called. Response fills whether returns true or fallse.
        public string ReponseString { get; internal set; }
        public string Status { get; internal set; }
        public string Message { get; internal set; }

        private readonly string TelnyxAPIUrl = "https://sms.telnyx.com/messages";

        private static Dictionary<string, string> values;

        public bool IsQueued = false;

        public TelnyxRestClient()
        {
        }

        public TelnyxRestClient(string XProfileSecret)
        {
            this.XProfileSecret = XProfileSecret;
        }

        public async System.Threading.Tasks.Task SendSMSAsync(Message msg)
        {
            //Try POST to Telnyx API
            try
            {
                MessageClientSetup(msg);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = await client.PostAsync(TelnyxAPIUrl, content);

                //Await for the response to finish
                ReponseString = await response.Content.ReadAsStringAsync();

                //Parse response into JSON object
                JObject o = JObject.Parse(ReponseString);

                //Get the status from the response - status is returned for both failed and successful messages
                Status = (string)o["status"];

                //Set IsQueued to true if Status is queueud
                IsQueued |= Status == "queued";

                //Return our own message if it is not generated
                Message = (IsQueued) ?  "Message queued" : (string)o["message"];
            }
            //Something wrong with the request
            catch (Exception x) 
            { 
                throw x;
            }
        }

        private void MessageClientSetup(Message msg)
        {
            //Set header
            client.DefaultRequestHeaders.Add("x-profile-secret", XProfileSecret);

            //Add values to a dictionary for pipelineing into the FormUrlEncodedContent
            values = new Dictionary<string, string>
            {
               { "from", msg.FromPhoneNumber },
               { "to", msg.ToPhoneNumber },
               { "body", msg.Body }
            };

            //Add delivery_status_webhook_url to the values if it is specified
            if (msg.DeliveryStatusWebhookUrl != null) values.Add("delivery_status_webhook_url", msg.DeliveryStatusWebhookUrl);

            //Add delivery_status_failover_url to the values if it is specified
            if (msg.DeliveryStatusFailoverUrl != null) values.Add("delivery_status_failover_url", msg.DeliveryStatusFailoverUrl);

            //Add check_sender_health to the values if it is specified
            if (msg.CheckSenderHealth) values.Add("check_sender_health", "true");
        }
    }
}
