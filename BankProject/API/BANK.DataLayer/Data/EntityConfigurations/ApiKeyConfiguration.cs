using BANK.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.API.Data.EntitiesConfigurations
{
    public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.Property(a => a.key)
                 .IsRequired()
                 .HasMaxLength(128);
        }
    }
}
