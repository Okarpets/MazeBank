using Azure.Messaging.ServiceBus;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BANK.BusinessLayer.Services.Classes;

public class BusService : IBusService
{
    private readonly string _connectionString;

    private readonly string _serviceBusSender;

    public BusService(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("ServiceBus:ServiceBusConnection");

        _serviceBusSender = configuration.GetValue<string>("ServiceBus:ServiceBusSender");

        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new ArgumentNullException(nameof(_connectionString), "Service Bus connection string is missing.");
        }

        if (string.IsNullOrEmpty(_serviceBusSender))
        {
            throw new ArgumentNullException(nameof(_connectionString), "Service Bus sender string is missing.");
        }
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

        var busClient = new ServiceBusClient(_connectionString);
        var sender = busClient.CreateSender(_serviceBusSender);
        var messageBus = new ServiceBusMessage(data);

        await sender.SendMessageAsync(messageBus);
    }
}
