# SharpNyx
Telnyx API C# wrapper for .Net Core. Uses  HttpClient.

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
TelnyxRestClient rc = new TelnyxRestClient("Q7EI8KGZJ3FrwBxMKq5zmID1");

//Call and wait for SendSMS to finish
rc.SendSMSAsync(new Message("+16508976777", "+16506003337", "Hello Telnyx")).Wait();

//Check to see if it is queued
bool isq = rc.IsQueued;

//rc.Message returns "Message queued" if successful, returns the message if unsuccessful delivery
Console.WriteLine(rc.Message);
```

#### Response Payload
```csharp
//Get the full response payload on a successful message
ResponsePayload rpl = ResponsePayload.FromJson(rc.ReponseString);

//Print SMS Id generated from the response
Console.WriteLine(ResponsePayload.FromJson(rc.ReponseString).SMSId);
```


MIT License
2019 Bharat Bhardwaj