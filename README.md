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

//Quick instantiation
TelnyxRestClient trc = new TelnyxRestClient("PnuROvNNFSMvHStXdBGzgoR9", "+16508976777", "+16506003337", "Hello Telnyx");

//Call and wait for Async method to finish
rc.SendSMSAsync().Wait();

//Check to see if it is queued
bool isq = rc.IsQueued;

//Print SMS Id generated from the response
Console.WriteLine(ResponsePayload.FromJson(rc.ReponseString).SMSId);

//rc.Message returns Queued if successful, returns the message if unsuccessful delivery
Console.WriteLine(rc.Message);
```

#### Response Payload
```csharp
//Get the full response payload on a successful message
ResponsePayload rpl = ResponsePayload.FromJson(rc.ReponseString);
```


MIT License
2019 Bharat Bhardwaj