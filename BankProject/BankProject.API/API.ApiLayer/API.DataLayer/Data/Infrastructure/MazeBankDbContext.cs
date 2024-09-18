using API.DataLayer.Data.EntityConfigurations;
using API.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DataLayer.Data.Infrastructure
{
    public class MazeBankDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<AccountEntity> Accounts { get; set; }

        public DbSet<TransactionEntity> Transactions { get; set; }

        public MazeBankDbContext(DbContextOptions<MazeBankDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
