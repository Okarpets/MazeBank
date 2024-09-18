using API.DataLayer.Data.Infrastructure;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Entities;

namespace API.DataLayer.Data.Repositories.Realization
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(MazeBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
