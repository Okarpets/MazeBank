namespace BANK.DataLayer.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    IAccountRepository AccountRepository { get; }

    ITransactionRepository TransactionRepository { get; }

    Task SaveChangesAsync();
}
