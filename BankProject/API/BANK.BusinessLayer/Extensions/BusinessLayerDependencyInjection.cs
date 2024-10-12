using BANK.BusinessLayer.Services.Classes;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BANK.BusinessLayer.Extensions;

public static class BusinessLayerDependencyInjection
{
    public static void AddBusinessLogicLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionPrivateService, TransactionPrivateService>();
    }
}