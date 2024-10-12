using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using DataLayer.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data.Repositories.Realization;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    private readonly BankDbContext _dbContext;

    public AccountRepository(BankDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> FindByNumber(string accountNumber)
    {
        return await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }

    public async Task DeleteByNumber(string accountNumber)
    {
        var account = await FindByNumber(accountNumber);
        if (account != null)
        {
            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
