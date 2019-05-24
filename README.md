# SharpNyx
C# wrapper for Telnyx API

//Sample code to send message
using Telnyx.SharpNyx;


TelnyxRestClient rc = new TelnyxRestClient("PnuROvNNFSMvHStXdBGzgoR9", "+16508976777", "+16506003337", "Hello Telnyx");

rc.SendSMSAsync().Wait();

Console.WriteLine(ResponsePayload.FromJson(rc.ReponseString).SMSId);