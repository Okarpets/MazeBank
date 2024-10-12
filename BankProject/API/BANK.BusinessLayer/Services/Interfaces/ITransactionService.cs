using Bank.API.Models;
using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionResult> TransferMoney(string fromAccountId, string toAccountId, decimal amount);

    Task<TransactionResult> WithdrawAsync(Guid accountId, string amount);

    Task<TransactionResult> DepositAsync(Guid accountId, string amount);

    Task<TransactionResult> WithdrawAdminAsync(string accountNumber, string amount);

    Task<TransactionResult> DepositAdminAsync(string accountNumber, string amount);

    Task<IEnumerable<Transaction>> GetTransactionsByAccountId(Guid accountId);

    Task<Transaction> GetTransactionDetails(Guid transactionId);

    Task DeleteTransactionsByAccountId(Guid accountId);
}
