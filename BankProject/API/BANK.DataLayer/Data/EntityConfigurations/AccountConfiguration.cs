using BANK.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.API.Data.EntitiesConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Balance)
                .HasColumnType("decimal(18, 2)");

            builder.HasMany(a => a.FromTransactions)
                   .WithOne(td => td.FromAccount)
                   .HasForeignKey(td => td.FromAccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.ToTransactions)
                   .WithOne(td => td.ToAccount)
                   .HasForeignKey(td => td.ToAccountId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
