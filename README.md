# SharpNyx
Telnyx API C# wrapper for .Net Core.

### Dependencies
* .NETCoreApp 2.1
* Newtonsoft.Json (>= 12.0.2)

### Installation
You can add the NuGet package or download and reference the Telnyx.dll issued in Releases.

### Usage
#### Send a message
```csharp
using Telnyx.SharpNyx;


TelnyxRestClient trc = new TelnyxRestClient("PnuROvNNFSMvHStXdBGzgoR9", "+16508976777", "+16506003337", "Hello Telnyx");

rc.SendSMSAsync().Wait();

Console.WriteLine(ResponsePayload.FromJson(rc.ReponseString).SMSId);
```

MIT License
2019 Bharat Bhardwaj