using DataLayer.Data.Infrastructure;

namespace BANK.DataLayer.Data;

public static class DbInitializer
{
    public static void InitializeDataBase(BankDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();
    }
}
