using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using DataLayer.Data.Infrastructure;

namespace DataLayer.Data.Repositories.Realization;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly BankDbContext _dbContext;

    public TransactionRepository(BankDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RemoveRange(IEnumerable<Transaction> transactions)
    {
        _dbContext.Set<Transaction>().RemoveRange(transactions);
        await _dbContext.SaveChangesAsync();
    }
}
