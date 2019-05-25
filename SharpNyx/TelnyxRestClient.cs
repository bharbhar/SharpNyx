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

        public bool IsQueued = false;

        public TelnyxRestClient()
        {
        }

        public TelnyxRestClient(string XProfileSecret)
        {
            this.XProfileSecret = XProfileSecret;

            //Add secret to client header
            client.DefaultRequestHeaders.Add("x-profile-secret", XProfileSecret);
        }

        public async System.Threading.Tasks.Task SendSMSAsync(Message msg)
        {
            //Try POST to Telnyx API
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(msg.MessageClientDictionary());

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
    }
}
