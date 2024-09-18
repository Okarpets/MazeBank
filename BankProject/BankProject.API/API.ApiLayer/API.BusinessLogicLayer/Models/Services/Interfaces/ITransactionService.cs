using API.DataLayer.Models;

namespace API.BusinessLogicLayer.Models.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionModel>> GetAllAsync();

        Task<Guid> AddAsync(TransactionModel model);

        Task<Guid> UpdateAsync(TransactionModel model);

        Task DeleteAsync(Guid id);

        Task<TransactionModel> GetByIdAsync(Guid id);
    }
}
