namespace BANK.BusinessLayer.Services.Interfaces;

public interface IBusService
{
    public Task messageAsync(string FromAccount, string ToAccount, string Amount, string Email, Guid OrderId, string Status);
}
