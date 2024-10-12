using BANK.DataLayer.Entities;

namespace BANK.DataLayer.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> DeleteByUsername(string username);

    Task<User> GetUserWithTransactions(Guid userId);

    Task<User> FindByUsername(string username);

    Task<bool> Exists(string username);
}
