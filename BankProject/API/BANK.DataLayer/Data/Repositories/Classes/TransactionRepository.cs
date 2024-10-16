using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using DataLayer.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data.Repositories.Realization;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly BankDbContext _dbContext;

    public TransactionRepository(BankDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RemoveRange(Guid accountId)
    {
        var transactionsToRemove = await _dbContext.Set<Transaction>()
        .Where(t => t.AccountId == accountId)
        .ToListAsync();

        if (transactionsToRemove.Any())
        {
            _dbContext.Set<Transaction>().RemoveRange(transactionsToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}
