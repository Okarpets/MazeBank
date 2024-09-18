using API.DataLayer.Models;

namespace API.BusinessLogicLayer.Models.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountModel>> GetAllAsync();

        Task<Guid> AddAsync(AccountModel model);

        Task<Guid> UpdateAsync(AccountModel model);

        Task DeleteAsync(Guid id);

        Task<AccountModel> GetByIdAsync(Guid id);
    }
}
