using BANK.BusinessLayer.Services.Interfaces;
using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Classes
{
    public class TransactionPrivateService : ITransactionPrivateService
    {
        public TransactionPrivateService() { }

        public async Task<(Transaction, TransactionDetails)> CreateTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = fromAccount.Id
            };

            var transactionDetails = new TransactionDetails
            {
                Id = transaction.Id,
                FromAccountId = fromAccount.Id,
                ToAccountId = toAccount.Id,
                Amount = amount,
                TransactionType = 3,
                TransactionDate = DateTime.UtcNow.ToLocalTime(),
                FromAccountNumber = fromAccount.AccountNumber,
                ToAccountNumber = toAccount.AccountNumber,
            };

            return (transaction, transactionDetails);
        }

        public async Task<(Transaction, TransactionDetails)> CreateWithdraw(Account account, decimal amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id
            };

            var transactionDetails = new TransactionDetails
            {
                Id = transaction.Id,
                FromAccountId = account.Id,
                ToAccountId = account.Id,
                Amount = amount,
                TransactionType = 2,
                FromAccountNumber = account.AccountNumber,
                ToAccountNumber = account.AccountNumber,
                TransactionDate = DateTime.UtcNow.ToLocalTime()
            };

            return (transaction, transactionDetails);
        }

        public async Task<(Transaction, TransactionDetails)> CreateDeposit(Account account, decimal amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id
            };

            var transactionDetails = new TransactionDetails
            {
                Id = transaction.Id,
                FromAccountId = account.Id,
                ToAccountId = account.Id,
                Amount = amount,
                TransactionType = 1,
                FromAccountNumber = account.AccountNumber,
                ToAccountNumber = account.AccountNumber,
                TransactionDate = DateTime.UtcNow.ToLocalTime()
            };

            return (transaction, transactionDetails);
        }
    }
}
