using BANK.DataLayer.Data.Repositories.Interfaces;
using DataLayer.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data.Repositories.Realization;

public class TransactionDetailsRepository : Repository<TransactionDetails>, ITransactionDetailsRepository
{
    private readonly BankDbContext _dbContext;

    public TransactionDetailsRepository(BankDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RemoveRange(Guid accountId)
    {
        var transactionsToRemoveFrom = await _dbContext.Set<TransactionDetails>()
        .Where(t => t.FromAccountId == accountId)
        .ToListAsync();

        if (transactionsToRemoveFrom.Any())
        {
            _dbContext.Set<TransactionDetails>().RemoveRange(transactionsToRemoveFrom);
            await _dbContext.SaveChangesAsync();
        }

        var transactionsToRemoveTo = await _dbContext.Set<TransactionDetails>()
        .Where(t => t.ToAccountId == accountId)
        .ToListAsync();

        if (transactionsToRemoveTo.Any())
        {
            _dbContext.Set<TransactionDetails>().RemoveRange(transactionsToRemoveTo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
