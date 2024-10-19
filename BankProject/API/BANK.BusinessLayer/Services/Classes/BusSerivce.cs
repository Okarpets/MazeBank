using Azure.Messaging.ServiceBus;
using BANK.API.Models;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BANK.BusinessLayer.Services.Classes;

public class BusSerivce : IBusService
{
    private readonly API.Models.ServiceBusSettings _settings;

    public BusSerivce(IOptions<ServiceBusSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task messageAsync(string FromAccount, string ToAccount, string Amount, string Email, Guid OrderId, string Status)
    {
        var message = new
        {
            FromAccount = FromAccount,
            ToAccount = ToAccount,
            Amount = Amount,
            Email = Email,
            OrderId = OrderId,
            Status = Status
        };

        var data = JsonSerializer.Serialize(message);

        var busClient = new ServiceBusClient(_settings.ServiceBusConnection);
        var sender = busClient.CreateSender(_settings.ServiceBusSender);
        var messageBus = new ServiceBusMessage(data);

        await sender.SendMessageAsync(messageBus);
    }
}
