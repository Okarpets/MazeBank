using API.BusinessLogicLayer.Models.Services.Interfaces;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Entities;
using API.DataLayer.Models;
using AutoMapper;

namespace API.BusinessLogicLayer.Models.Services.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(TransactionModel model)
        {
            var clientRepository = _unitOfWork.TransactionRepository;

            var client = _mapper.Map<TransactionEntity>(model);

            var result = await clientRepository.Create(client);
            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientRepository = _unitOfWork.TransactionRepository;
            await clientRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetAllAsync()
        {
            var clientRepository = _unitOfWork.TransactionRepository;
            return _mapper.Map<IEnumerable<TransactionModel>>(clientRepository.GetAll().ToList());
        }

        public async Task<TransactionModel> GetByIdAsync(Guid id)
        {
            var clientRepository = _unitOfWork.TransactionRepository;
            var client = clientRepository.Find(id);
            return _mapper.Map<TransactionModel>(client);
        }

        public async Task<Guid> UpdateAsync(TransactionModel model)
        {
            var clientRepository = _unitOfWork.TransactionRepository;
            var client = await clientRepository.Find(model.Id);

            _mapper.Map(model, client);

            var result = await clientRepository.Update(client);

            await _unitOfWork.SaveChangesAsync();

            return result.Id;
        }
    }
}
