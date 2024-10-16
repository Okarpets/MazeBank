using Bank.API.Data.EntitiesConfigurations;
using BANK.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data.Infrastructure;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<ApiKey> ApiKeys { get; set; }

    public DbSet<TransactionDetails> TransactionDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionDetails>()
        .HasOne(td => td.FromAccount)
        .WithMany(a => a.FromTransactions)
        .HasForeignKey(td => td.FromAccountId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TransactionDetails>()
            .HasOne(td => td.ToAccount)
            .WithMany(a => a.ToTransactions)
            .HasForeignKey(td => td.ToAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new ApiKeyConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
