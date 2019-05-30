//2019 Bharat Bhardwaj Bugs Inc. of California
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Telnyx.SharpNyx
{
    /// <summary>
    /// The MessagingAPIClient TAC is the main broker for the HttpClient. TAC does the work of sending messages, and communicating with the Telnyx Rest API.
    /// </summary>
    public class MessagingAPIClient
    {
        //One client per application instance
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly string TelnyxAPIUrl = "https://sms.telnyx.com/messages";
        /// <summary>
        /// Account XProfileSecret found in Message Profiles
        /// </summary>
        public string XProfileSecret { get; set; }
        //Response is null until SendSMSAsync is called. Response fills whether returns true or false.
        public string ReponseString { get; internal set; }
        public string ReponseStatus { get; internal set; }
        public string ReponseMessage { get; internal set; }
        public HttpResponseMessage HttpResponse { get; internal set; }

        public MessagingAPIClient()
        {
        }

        public MessagingAPIClient(string XProfileSecret)
        {
            this.XProfileSecret = XProfileSecret;
        }

        public async System.Threading.Tasks.Task SendSMSAsync(SMS sms)
        {
            await SendMessage(sms);
        }

        public async System.Threading.Tasks.Task SendMMSAsync(MMS mms)
        {
            await SendMessage(mms);
        }

        private async System.Threading.Tasks.Task SendMessage(Message msg)
        {
            //Try POST to Telnyx API
            try
            {
                //Add x-profile-secret to client header every time before sending
                httpClient.DefaultRequestHeaders.Add("x-profile-secret", XProfileSecret);

                //Make Http call get response
                HttpResponse = await httpClient.PostAsync(TelnyxAPIUrl, new StringContent(JsonConvert.SerializeObject(msg), System.Text.Encoding.UTF8, "application/json"));

                //Await for the response to finish
                ReponseString = await HttpResponse.Content.ReadAsStringAsync();

                //Parse response into JSON object
                JObject o = JObject.Parse(ReponseString);

                //Get the status from the response - status is returned for both failed and successful messages
                ReponseStatus = (string)o["status"];

                //Set IsQueued to true if Status is queueud
                msg.IsQueued |= ReponseStatus == "queued";

                //Return our own message if it is not generated
                ReponseMessage = (msg.IsQueued) ? "Queued" : (string)o["message"];
            }
            //Something wrong with the request
            catch (Exception x)
            {
                throw x;
            }
        }

        public async System.Threading.Tasks.Task<MessageDeliveryRecord> GetMessageDeliveryRecord(string msgid)
        {
            //Try Get MDR
            try
            {
                //Add x-profile-secret to client header every time before sending
                httpClient.DefaultRequestHeaders.Add("x-profile-secret", XProfileSecret);

                //Make Http call get response
                HttpResponse = await httpClient.GetAsync(TelnyxAPIUrl+"/"+msgid);

                //Await for the response to finish
                ReponseString = await HttpResponse.Content.ReadAsStringAsync();

                return MessageDeliveryRecord.FromJson(ReponseString);
            }
            //Something wrong with the request
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
