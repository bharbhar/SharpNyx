# SharpNyx
Telnyx API C# wrapper for .Net Core. Uses  HttpClient.

### Dependencies
* .NETCoreApp 2.1
* Newtonsoft.Json (>= 12.0.2)

It should work with .Net standard but you may have to manually import the Telnyx.dll.

### Installation
Add the NuGet package or download and reference the Telnyx.dll issued in Releases.
https://www.nuget.org/packages/SharpNyx

### Usage
#### Send a message
```csharp
using Telnyx.SharpNyx;

//Quick instantiation
MessagingAPIClient mac = new MessagingAPIClient("Q7EI8KGZJ3FrwBxMKq5zmID1");

//Make a new message
Message msg = new Message("BUGSINC", "+16506003337", "Hello Telnyx");

//Call and wait for SendSMS to finish
mac.SendSMSAsync(msg).Wait();

//Check to see if it is queued
bool isq = msg.IsQueued;
```
#### The Messaging API Client (mac)
Mac is the main broker for the HttpClient. mac does the work of sending messages, and communicating with the Telnyx Rest API.

Mac implements a static HttpClient to use for the entire application. You can instantiate the MessagingAPIClient by directly adding the x-secret.

The mac will add the secret to the header every time a new request is made. So you can change the secret for the same instance.

```csharp
//Get the full Http response from the API call
string httpstatus = mac.HttpResponse.StatusCode.ToString(); //returns OK for 200

//Reponse Status returns the status. This field is used to determine the Message.IsQueued value.
string responsemessage = mac.ReponseStatus;

//Reponse Message returns "Queued" if successful, returns the message if unsuccessful delivery
string responsemessage = mac.ReponseMessage;
```

#### Accepted Response Payload
```csharp
//Get the full response payload on an accepted outbound message request
AcceptedResponsePayload arp = AcceptedResponsePayload.FromJson(mac.ReponseString);
string smsid = arp.SMSId; //Generated message ID from Telnyx
```

#### Rejected Response Payload
```csharp
//Get the response error details from the mac response string if message is not queued
RejectedResponsePayload rrp = RejectedResponsePayload.FromJson(mac.ReponseString);
string errormessage = err.Message;
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