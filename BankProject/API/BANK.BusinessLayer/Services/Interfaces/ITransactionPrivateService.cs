using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Interfaces;

public interface ITransactionPrivateService
{
    Task<(Transaction, TransactionDetails)> CreateWithdraw(Account account, decimal amount);

    Task<(Transaction, TransactionDetails)> CreateDeposit(Account account, decimal amount);

    Task<(Transaction, TransactionDetails)> CreateTransfer(Account fromAccount, Account toAccount, decimal amount);
}
