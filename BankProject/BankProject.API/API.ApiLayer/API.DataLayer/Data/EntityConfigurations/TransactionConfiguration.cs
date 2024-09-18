using API.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.DataLayer.Data.EntityConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .IsRequired();

            builder.Property(x => x.Amount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

            builder.Property(x => x.TransactionDate)
                    .IsRequired();

            builder.Property(x => x.Description)
                    .HasMaxLength(255);

            builder.Property(x => x.Status)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(x => x.ReceiptPath)
                    .HasMaxLength(255);

            builder.HasOne(x => x.FromAccount)
                    .WithMany(a => a.SentTransactions)
                    .HasForeignKey(x => x.FromAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ToAccount)
                    .WithMany(a => a.ReceivedTransactions)
                    .HasForeignKey(x => x.ToAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



