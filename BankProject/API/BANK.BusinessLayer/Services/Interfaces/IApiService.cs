namespace BANK.BusinessLayer.Services.Interfaces;

public interface IApiService
{
    Task<string> GenerateApiKey();

    Task<bool> ExistsApiKey(string apiKey);
}
