using API.DataLayer.Models;

namespace API.BusinessLogicLayer.Models.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllAsync();

        Task<Guid> AddAsync(UserModel model);

        Task<Guid> UpdateAsync(UserModel model);

        Task DeleteAsync(Guid id);

        Task<UserModel> GetByIdAsync(Guid id);
    }
}
