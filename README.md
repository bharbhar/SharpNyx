# SharpNyx
Telnyx API C# wrapper for .Net Core. Uses  HttpClient.

### Dependencies
* .NETCoreApp 2.1
* Newtonsoft.Json (>= 12.0.2)

It should work with .Net standard but you may have to manually import the Telnyx.dll.

### Installation
You can add the NuGet package or download and reference the Telnyx.dll issued in Releases.
https://www.nuget.org/packages/SharpNyx

### Usage
#### Send a message
```csharp
using Telnyx.SharpNyx;

//Quick instantiation
TelnyxRestClient trc = new TelnyxRestClient("Q7EI8KGZJ3FrwBxMKq5zmID1");

//Call and wait for SendSMS to finish
trc.SendSMSAsync(new Message("BUGSINC", "+16506003337", "Hello Telnyx")).Wait();

//Get the full Http response from the API call
string httpstatus = trc.HttpResponse.StatusCode.ToString(); //returns OK for 200

//Check to see if it is queued
bool isq = trc.IsQueued;

//trc.Message returns "Message queued" if successful, returns the message if unsuccessful delivery
string responsemessage = trc.Message;
```

#### Response Payload
```csharp
//Get the full response payload on a successful message
ResponsePayload rpl = ResponsePayload.FromJson(trc.ReponseString);
```

#### Response Error
```csharp
//Get the response error details from the TRC response string if message is not queued
ResponseError rpl = ResponseError.FromJson(trc.ReponseString);
```

#### Outgoing Message Can be generated without specifying a source
```csharp
//Send a message with just the recipient and body
Message msg = new Message();
msg.ToPhoneNumber = "+16506003337";
msg.Body = "Hello Telnyx";
```


MIT License
2019 Bharat Bhardwaj