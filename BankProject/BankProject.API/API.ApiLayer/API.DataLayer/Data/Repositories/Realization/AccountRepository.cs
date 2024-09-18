using API.DataLayer.Data.Infrastructure;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Entities;

namespace API.DataLayer.Data.Repositories.Realization
{
    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(MazeBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
