namespace BANK.DataLayer.Entities;

public class Transaction : IEntity
{
    public Guid AccountId { get; set; }

    public virtual TransactionDetails TransactionDetails { get; set; }
}

