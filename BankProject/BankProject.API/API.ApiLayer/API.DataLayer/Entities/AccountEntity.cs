namespace API.DataLayer.Entities
{
    public class AccountEntity : IEntity
    {

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TransactionEntity> SentTransactions { get; set; }
        public ICollection<TransactionEntity> ReceivedTransactions { get; set; }
    }
}