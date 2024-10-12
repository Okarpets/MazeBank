using BANK.BusinessLayer.Services.Interfaces;
using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using System.Text;

namespace BANK.BusinessLayer.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Account> CreateAccount(Guid userId)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                AccountNumber = GenerateRandomCardNumber(),
                Balance = 0,
                UserId = userId
            };

            await _accountRepository.Create(account);
            return account;
        }

        public async Task<string> GetAccountNumberByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.Find(accountId);
            return account?.AccountNumber;
        }

        public async Task<Account> GetUserAccountByNumber(string accountNumber)
        {
            return await _accountRepository.FindByNumber(accountNumber);
        }

        public async Task<Account> GetUserAccountById(Guid accountId)
        {
            return await _accountRepository.Find(accountId);
        }
        public async Task<List<Account>> GetAllUserAccountsById(Guid accountId)
        {
            return await _accountRepository.GetAllAsync(a => a.UserId == accountId);
        }

        public async Task<bool> DeleteAccountById(Guid accountId)
        {
            var account = await _accountRepository.Find(accountId);
            if (account == null)
            {
                return false;
            }

            await _accountRepository.Delete(accountId);
            await _accountRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAccountBuNumber(string accountNumber)
        {
            var account = await _accountRepository.FindByNumber(accountNumber);
            if (account == null)
            {
                return false;
            }

            await _accountRepository.DeleteByNumber(accountNumber);
            await _accountRepository.SaveChangesAsync();

            return true;
        }
        private string GenerateRandomCardNumber()
        {
            var random = new Random();
            var cardNumber = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                if (i > 0)
                    cardNumber.Append("-");
                for (int j = 0; j < 4; j++)
                {
                    cardNumber.Append(random.Next(0, 10));
                }
            }

            return cardNumber.ToString();
        }
    }
}
