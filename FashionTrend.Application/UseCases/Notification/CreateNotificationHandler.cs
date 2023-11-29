using System;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

public class CreateNotificationHandler
{
    private string AccountsID;
    private string AuthToken;
    private string TwilioPhoneNumber;

    public CreateNotificationHandler(
        string _accountId,
        string _authToken,
        string _twilioPhoneNumber)
    {
        AccountsID = _accountId;
        AuthToken = _authToken;
        TwilioPhoneNumber = _twilioPhoneNumber;
    }

    public void SmsNotifier(string accountId, string authToken, string twilioPhoneNumber)
    {
        this.AccountsID = accountId;
        this.AuthToken = authToken;
        this.TwilioPhoneNumber = twilioPhoneNumber;
    }

    public void SendSMS(string toPhoneNumber, string message)
    {
        try
        {
            var messageResponse = SendMessage(toPhoneNumber, message);
            throw new Exception ("SMS sent successfully");
        }
        catch (Exception ex)
        {
            throw new Exception ($"Error sending SMS: {ex.Message}");

        }
    }

    private MessageResource SendMessage(string toPhoneNumber, string message)
    {
        TwilioClient.Init(AccountsID, AuthToken);
        var messageOption = new CreateMessageOptions(new Twilio.Types.PhoneNumber(toPhoneNumber))
        {
            Body = message,
            From = new Twilio.Types.PhoneNumber(TwilioPhoneNumber)
        };

        return MessageResource.Create(messageOption);
    }
}
