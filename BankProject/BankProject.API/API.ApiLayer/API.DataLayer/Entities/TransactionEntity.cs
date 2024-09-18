namespace API.DataLayer.Entities
{
    public class TransactionEntity : IEntity
    {

        public Guid FromAccountId { get; set; }
        public AccountEntity FromAccount { get; set; }

        public Guid ToAccountId { get; set; }
        public AccountEntity ToAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string ReceiptPath { get; set; }
    }
}
