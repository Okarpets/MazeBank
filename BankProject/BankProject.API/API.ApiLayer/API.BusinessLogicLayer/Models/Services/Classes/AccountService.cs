using API.BusinessLogicLayer.Models.Services.Interfaces;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Entities;
using API.DataLayer.Models;
using AutoMapper;

namespace API.BusinessLogicLayer.Models.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AccountModel model)
        {
            var clientRepository = _unitOfWork.AccountRepository;

            var client = _mapper.Map<AccountEntity>(model);

            var result = await clientRepository.Create(client);
            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientRepository = _unitOfWork.AccountRepository;
            await clientRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountModel>> GetAllAsync()
        {
            var clientRepository = _unitOfWork.AccountRepository;
            return _mapper.Map<IEnumerable<AccountModel>>(clientRepository.GetAll().ToList());
        }

        public async Task<AccountModel> GetByIdAsync(Guid id)
        {
            var clientRepository = _unitOfWork.AccountRepository;
            var client = clientRepository.Find(id);
            return _mapper.Map<AccountModel>(client);
        }

        public async Task<Guid> UpdateAsync(AccountModel model)
        {
            var clientRepository = _unitOfWork.AccountRepository;
            var client = await clientRepository.Find(model.Id);

            _mapper.Map(model, client);

            var result = await clientRepository.Update(client);

            await _unitOfWork.SaveChangesAsync();

            return result.Id;
        }
    }
}
