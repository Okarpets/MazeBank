namespace BANK.DataLayer.Entities;

public class Account : IEntity
{
    public string AccountNumber { get; set; }

    public decimal Balance { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<Transaction> FromTransactions { get; set; }

    public virtual ICollection<Transaction> ToTransactions { get; set; }
}
