using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using DataLayer.Data.Infrastructure;

namespace DataLayer.Data.Repositories.Realization;

public class ApiRepository : Repository<ApiKey>, IApiRepository
{
    private readonly BankDbContext _dbContext;

    public ApiRepository(BankDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
