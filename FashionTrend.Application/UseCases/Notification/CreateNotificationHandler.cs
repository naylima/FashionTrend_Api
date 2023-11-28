using System;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace FashionTrend.Application.UseCases.Notification;

public class CreateNotificationHandler
{
    private string AccountsID;
    private string AuthToken;
    private string TwilioPhoneNumber;
    private readonly ILogger<CreateNotificationHandler> _logger;

    public CreateNotificationHandler(
        string _accountId,
        string _authToken,
        string _twilioPhoneNumber,
        ILogger<CreateNotificationHandler> logger)
    {
        AccountsID = _accountId;
        AuthToken = _authToken;
        TwilioPhoneNumber = _twilioPhoneNumber;
        _logger = logger;
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
            _logger.LogInformation("SMS sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending SMS: {ex.Message}");

        }
        // buscando o retorno que veio da requisição do twillio

        // verificar se a notificação foi enviada
        // se não enviar, criar uma log, para ser feita a retentativa

        // construir tratatmento de erros
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
