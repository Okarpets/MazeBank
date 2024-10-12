using BANK.DataLayer.Data.Repositories.Interfaces;

namespace DataLayer.Data.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private BankDbContext _dbContext;

    public IUserRepository UserRepository { get; }

    public IAccountRepository AccountRepository { get; }

    public ITransactionRepository TransactionRepository { get; }


    public UnitOfWork
        (BankDbContext dbContext, IUserRepository userRepository,
         IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        AccountRepository = accountRepository;
        TransactionRepository = transactionRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
