namespace BANK.DataLayer.Entities;

public class User : IEntity
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public ICollection<Account> Accounts { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}