using BANK.DataLayer.Entities;

namespace BANK.DataLayer.Data.Repositories.Interfaces;
public interface IAccountRepository : IRepository<Account>
{
    Task<Account> FindByNumber(string accountNumber);

    Task DeleteByNumber(string accountNumber);
}
