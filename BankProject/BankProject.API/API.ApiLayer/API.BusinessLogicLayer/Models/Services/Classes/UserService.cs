using API.BusinessLogicLayer.Models.Services.Interfaces;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Entities;
using API.DataLayer.Models;
using AutoMapper;

namespace API.BusinessLogicLayer.Models.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(UserModel model)
        {
            var clientRepository = _unitOfWork.UserRepository;

            var client = _mapper.Map<UserEntity>(model);

            var result = await clientRepository.Create(client);
            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientRepository = _unitOfWork.UserRepository;
            await clientRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var clientRepository = _unitOfWork.UserRepository;
            return _mapper.Map<IEnumerable<UserModel>>(clientRepository.GetAll().ToList());
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var clientRepository = _unitOfWork.UserRepository;
            var client = clientRepository.Find(id);
            return _mapper.Map<UserModel>(client);
        }

        public async Task<Guid> UpdateAsync(UserModel model)
        {
            var clientRepository = _unitOfWork.UserRepository;
            var client = await clientRepository.Find(model.Id);

            _mapper.Map(model, client);

            var result = await clientRepository.Update(client);

            await _unitOfWork.SaveChangesAsync();

            return result.Id;
        }
    }
}
