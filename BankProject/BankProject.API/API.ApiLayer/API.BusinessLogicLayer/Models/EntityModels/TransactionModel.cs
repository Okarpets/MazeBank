namespace API.DataLayer.Models
{
    public class TransactionModel : IEntityModel
    {

        public Guid FromAccountId { get; set; }
        public AccountModel FromAccount { get; set; }

        public Guid ToAccountId { get; set; }
        public AccountModel ToAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string ReceiptPath { get; set; }
    }
}
