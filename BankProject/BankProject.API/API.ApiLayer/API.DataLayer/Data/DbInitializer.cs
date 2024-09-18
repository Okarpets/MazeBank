using API.DataLayer.Data.Infrastructure;

namespace DataLayer.Data;

public static class DbInitializer
{
    public static void InitializeDataBase(MazeBankDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();
    }
}