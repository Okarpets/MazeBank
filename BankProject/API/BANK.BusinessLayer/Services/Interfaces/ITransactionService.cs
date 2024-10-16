using Bank.API.Models;
using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDetails>> GetTransactionsByAccountId(Guid accountId);

    Task<TransactionDetails> GetTransactionDetails(Guid transactionId);

    Task<TransactionResult> DepositAsync(Guid accountId, string amount);

    Task<TransactionResult> WithdrawAsync(Guid accountId, string amount);

    Task<TransactionResult> TransferMoney(string fromAccountId, string toAccountId, decimal amount);

    Task DeleteTransactionsByAccountId(Guid accountId);

    Task<TransactionResult> WithdrawAdminAsync(string accountNumber, string amount);

    Task<TransactionResult> DepositAdminAsync(string accountNumber, string amount);
}
