using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public void Qr()
    {
        var accountSid = "ACe55a5d5ffabf4b87606e661c170f06e1";
        var authToken = "b156f3513853a2157cf6f932a0f3e4a5";
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
            new PhoneNumber("+38344849354"));
        messageOptions.From = new PhoneNumber("+12562910748");
        messageOptions.Body = "hello there niggah";

        var message = MessageResource.Create(messageOptions);
        Console.WriteLine(message.Body);
    }
}