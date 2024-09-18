using API.BusinessLogicLayer.Mapping;
using API.BusinessLogicLayer.Models.Services.Classes;
using API.BusinessLogicLayer.Models.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace API.BusinessLogicLayer.Extensions
{
    public static class BLLDI
    {
        public static void AddBusinessLogicLayer(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperBLLProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
