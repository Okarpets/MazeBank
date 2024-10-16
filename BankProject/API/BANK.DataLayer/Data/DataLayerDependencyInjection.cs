using BANK.DataLayer.Data.Repositories.Interfaces;
using DataLayer.Data.Infrastructure;
using DataLayer.Data.Repositories.Realization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BANK.DataLayer.Data;

public static class DataLayerDependencyInjection
{
    public static void AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BankDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly("BANK.API");
                b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IApiRepository, ApiRepository>();
        services.AddScoped<ITransactionDetailsRepository, TransactionDetailsRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
