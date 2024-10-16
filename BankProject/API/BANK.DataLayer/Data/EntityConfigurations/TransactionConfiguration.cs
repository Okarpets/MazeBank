using BANK.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.API.Data.EntitiesConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(t => t.TransactionDetails)
                   .WithOne(td => td.Transaction)
                   .HasForeignKey<TransactionDetails>(td => td.Id);
        }
    }
}
