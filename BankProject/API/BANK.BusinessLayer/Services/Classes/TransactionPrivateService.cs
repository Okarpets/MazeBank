using BANK.BusinessLayer.Services.Interfaces;
using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Classes
{
    public class TransactionPrivateService : ITransactionPrivateService
    {
        public TransactionPrivateService() { }

        public async Task<Transaction> CreateTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                FromAccountId = fromAccount.Id,
                ToAccountId = toAccount.Id,
                Amount = amount,
                TransactionType = 3,
                Description = $"| Transfer from {fromAccount.AccountNumber} to {toAccount.AccountNumber} | @ | Date: {DateTime.UtcNow} |",
                TransactionDate = DateTime.Now
            };

            return transaction;
        }

        public async Task<Transaction> CreateWithdraw(Account account, decimal amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = 2,
                Description = $"| Withdraw transaction |@| Date: {DateTime.UtcNow} |",
                FromAccountId = account.Id,
                ToAccountId = account.Id
            };

            return transaction;
        }

        public async Task<Transaction> CreateDeposit(Account account, decimal amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = 1,
                Description = $"| Deposit transaction |@| Date: {DateTime.UtcNow} |",
                FromAccountId = account.Id,
                ToAccountId = account.Id
            };

            return transaction;
        }
    }
}
