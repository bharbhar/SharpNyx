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
//Import
using Telnyx.SharpNyx;

//Quick instantiation
MessagingAPIClient mac = new MessagingAPIClient("Q7EI8KGZJ3FrwBxMKq5zmID1");

//Make a new SMS message
SMS sms = new SMS("FIREDATE", "+16506003337", "Hello World");

//Call and wait for SendSMS to finish
mac.SendSMSAsync(sms).Wait();

//Check to see if it is queued
bool isq = sms.IsQueued;
```

#### SMS
```csharp
//Send a message with just the recipient and body
Message msg = new Message();
msg.ToPhoneNumber = "+16506003337";
msg.Body = "Hello Telnyx";

mac.SendMessage(msg).Wait();
```

#### Send Quick MMS
```csharp
//Quickly send one image without subject
MessagingAPIClient mac = new MessagingAPIClient("M7RI1KGBJ8FrwBxTKq3zmIN1");

string url = "https://nssdc.gsfc.nasa.gov/planetary/image/saturn.jpg";

MMS mms = new MMS("FIREDATE", "+16508976777", new Dictionary<string, string> { { MediaUtype.Image, url } });

mac.SendMMSAsync(mms).Wait();

return mms.IsQueued;
```

#### MMS With Two Images
```csharp
//Send two images with a subject
MessagingAPIClient mac = new MessagingAPIClient("M7RI1KGBJ8FrwBxTKq3zmIN1");

string url1 = "https://upload.wikimedia.org/wikipedia/commons/5/5f/HubbleDeepField.800px.jpg";
string url2 = "https://upload.wikimedia.org/wikipedia/commons/e/e1/M45map.jpg";

Dictionary<string, string> dic1 = new Dictionary<string, string>();
dic1.Add(MediaUtype.Image, url1);

Dictionary<string, string> dic2 = new Dictionary<string, string>();
dic2.Add(MediaUtype.Image, url2);

MMS mms = new MMS()
{
    FromPhoneNumber = "FIREDATE",
    Subject = "Deep Space",
    ToPhoneNumber = "+16508976777",
    MediaUrls = new List<Dictionary<string, string>>() { dic1, dic2 }
};

mac.SendMMSAsync(mms).Wait();

return mms.IsQueued;
```
#### The Messaging API Client (MAC)
MAC is the main broker for the HttpClient. MAC does the work of sending messages, and communicating with the Telnyx Rest API.

MAC implements a static HttpClient to use for the entire application. You can instantiate the MessagingAPIClient by directly adding the x-secret.

The MAC will add the secret to the header every time a new request is made. So you can change the secret for the same instance.

```csharp
//Get the full Http response from the API call
string httpstatus = mac.HttpResponse.StatusCode.ToString(); //returns OK for 200

//Reponse Status returns the status. This field is used to determine the Message.IsQueued value.
string responsemessage = mac.ReponseStatus;

//Reponse Message returns "Queued" if successful, returns the message if unsuccessful delivery
string responsemessage = mac.ReponseMessage;

//Grab System.Exception
mac.MACException;
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

#### Message Delivery Record
```csharp
//Returns null if there is an exception
MessageDeliveryRecord mdr = await trc.GetMessageDeliveryRecord(msgid);
return mdr.Errors;
```


MIT License
2019 Bharat Bhardwaj