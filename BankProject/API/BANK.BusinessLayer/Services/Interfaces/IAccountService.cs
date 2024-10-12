using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Interfaces;

public interface IAccountService
{
    Task<Account> GetUserAccountByNumber(string accountNumber);

    Task<Account> GetUserAccountById(Guid accountId);

    Task<bool> DeleteAccountBuNumber(string accountNumber);

    Task<string> GetAccountNumberByIdAsync(Guid accountId);

    Task<List<Account>> GetAllUserAccountsById(Guid accountId);

    Task<bool> DeleteAccountById(Guid accountId);

    Task<Account> CreateAccount(Guid userId);
}