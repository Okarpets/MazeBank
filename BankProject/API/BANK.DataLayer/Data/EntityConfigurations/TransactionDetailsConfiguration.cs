using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.API.Data.EntitiesConfigurations
{
    public class TransactionDetailsConfiguration : IEntityTypeConfiguration<TransactionDetails>
    {
        public void Configure(EntityTypeBuilder<TransactionDetails> builder)
        {
            builder.HasKey(td => td.Id);

            builder.Property(td => td.Amount)
                   .HasColumnType("decimal(18, 2)");

            builder.HasOne(td => td.FromAccount)
                   .WithMany(a => a.FromTransactions)
                   .HasForeignKey(td => td.FromAccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(td => td.ToAccount)
                   .WithMany(a => a.ToTransactions)
                   .HasForeignKey(td => td.ToAccountId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
