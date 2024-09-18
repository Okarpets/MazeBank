namespace API.DataLayer.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        IAccountRepository AccountRepository { get; }

        Task SaveChangesAsync();
    }
}
