using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Interfaces;

public interface ITransactionPrivateService
{
    Task<Transaction> CreateWithdraw(Account account, decimal amount);

    Task<Transaction> CreateDeposit(Account account, decimal amount);

    Task<Transaction> CreateTransfer(Account fromAccount, Account toAccount, decimal amount);
}
