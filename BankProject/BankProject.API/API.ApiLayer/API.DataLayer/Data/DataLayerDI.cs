using API.DataLayer.Data.Infrastructure;
using API.DataLayer.Data.Repositories.Interfaces;
using API.DataLayer.Data.Repositories.Realization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.Data
{
    public static class DataLayerDI
    {
        public static void AddDataAccessLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<MazeBankDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("API.ApiLayer"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}