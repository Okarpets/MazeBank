namespace BANK.DataLayer.Entities;

public class Transaction : IEntity
{
    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public int? TransactionType { get; set; }

    public string? Description { get; set; }

    public Guid? FromAccountId { get; set; }

    public virtual Account FromAccount { get; set; }

    public Guid? ToAccountId { get; set; }

    public virtual Account ToAccount { get; set; }
}