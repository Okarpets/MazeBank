using BANK.DataLayer.Entities;

namespace BANK.DataLayer.Data.Repositories.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task RemoveRange(IEnumerable<Transaction> transactions);
}