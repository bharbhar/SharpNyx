//2019 Bharat Bhardwaj Bugs Inc. of California
using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
namespace Telnyx.SharpNyx
{
    /// <summary>
    /// The TelnyxRestClient (TRC) 
    /// </summary>
    public class TelnyxRestClient
    {
        //One client per application instance
        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Account XProfileSecret found in Message Profiles
        /// </summary>
        public string XProfileSecret { get; set; }

        //Response is null until SendSMSAsync is called. Response fills whether returns true or false.
        public string ReponseString { get; internal set; }
        public string ReponseStatus { get; internal set; }
        public string ReponseMessage { get; internal set; }

        private readonly string TelnyxAPIUrl = "https://sms.telnyx.com/messages";

        public HttpResponseMessage HttpResponse { get; internal set; }
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
                //Add x-profile-secret to client header every time before sending
                httpClient.DefaultRequestHeaders.Add("x-profile-secret", XProfileSecret);

                //Make Http call get response
                HttpResponse = await httpClient.PostAsync(TelnyxAPIUrl, new FormUrlEncodedContent(msg.MessageClientDictionary()));

                //Await for the response to finish
                ReponseString = await HttpResponse.Content.ReadAsStringAsync();

                //Parse response into JSON object
                JObject o = JObject.Parse(ReponseString);

                //Get the status from the response - status is returned for both failed and successful messages
                ReponseStatus = (string)o["status"];

                //Set IsQueued to true if Status is queueud
                IsQueued |= ReponseStatus == "queued";

                //Return our own message if it is not generated
                ReponseMessage = (IsQueued) ? "Message queued" : (string)o["message"];
            }
            //Something wrong with the request
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
