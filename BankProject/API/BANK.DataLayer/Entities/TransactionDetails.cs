using BANK.DataLayer.Entities;

public class TransactionDetails : IEntity
{
    public decimal Amount { get; set; }

    public string FromAccountNumber { get; set; }

    public string ToAccountNumber { get; set; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public int TransactionType { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual Account FromAccount { get; set; }

    public virtual Account ToAccount { get; set; }

    public virtual Transaction Transaction { get; set; }
}
