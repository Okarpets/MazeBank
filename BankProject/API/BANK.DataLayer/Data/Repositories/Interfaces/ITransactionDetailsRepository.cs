using BANK.DataLayer.Entities;

namespace BANK.DataLayer.Data.Repositories.Interfaces;

public interface ITransactionDetailsRepository : IRepository<TransactionDetails>
{
    Task RemoveRange(Guid accountId);
}
