using API.DataLayer.Data.Repositories.Interfaces;

namespace API.DataLayer.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private MazeBankDbContext _dbContext;

        public IUserRepository UserRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public IAccountRepository AccountRepository { get; }


        public UnitOfWork
            (MazeBankDbContext dbContext, IUserRepository userRepository,
             ITransactionRepository transactionRepository,
             IAccountRepository accountRepository
             )
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            TransactionRepository = transactionRepository;
            AccountRepository = accountRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
