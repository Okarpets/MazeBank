namespace BANK.API.Models
{
    public class ServiceBusSettings
    {
        public string ServiceBusConnection { get; set; }

        public string ServiceBusSender { get; } = "MazeBusSender";
    }
}
