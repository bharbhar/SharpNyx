# SharpNyx
Telnyx API C# wrapper for .Net Core 2.1.

### Usage
```csharp
//Sample code to send message
using Telnyx.SharpNyx;


TelnyxRestClient trc = new TelnyxRestClient("PnuROvNNFSMvHStXdBGzgoR9", "+16508976777", "+16506003337", "Hello Telnyx");

rc.SendSMSAsync().Wait();

Console.WriteLine(ResponsePayload.FromJson(rc.ReponseString).SMSId);
```

MIT License
2019 Bharat Bhardwaj