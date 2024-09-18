namespace API.DataLayer.Models
{
    public class AccountModel : IEntityModel
    {

        public Guid UserId { get; set; }

        public UserModel User { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TransactionModel> SentTransactions { get; set; } = new List<TransactionModel>();
        public ICollection<TransactionModel> ReceivedTransactions { get; set; } = new List<TransactionModel>();
    }
}