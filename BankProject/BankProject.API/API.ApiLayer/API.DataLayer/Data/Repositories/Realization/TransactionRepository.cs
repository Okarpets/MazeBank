using API.DataLayer.Data.Infrastructure;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Entities;

namespace API.DataLayer.Data.Repositories.Realization
{
    public class TransactionRepository : Repository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(MazeBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
