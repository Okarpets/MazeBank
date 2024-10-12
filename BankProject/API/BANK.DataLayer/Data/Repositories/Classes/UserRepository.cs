using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using DataLayer.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data.Repositories.Realization;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly BankDbContext _dbContext;

    public UserRepository(BankDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> DeleteByUsername(string username)
    {
        var deleteEntity = await _dbContext.Set<User>()
                                            .FirstOrDefaultAsync(e => e.Username == username);
        if (deleteEntity != null)
        {
            _dbContext.Set<User>().Remove(deleteEntity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<User> GetUserWithTransactions(Guid userId)
    {
        return await _dbContext.Users
            .Include(u => u.Transactions)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User> FindByUsername(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(string username)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username);
    }
}
