using BANK.BusinessLayer.Services.Interfaces;
using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using System.Security.Cryptography;
using System.Text;

namespace BANK.BusinessLayer.Services.Classes
{
    public class ApiService : IApiService
    {
        private readonly IApiRepository _apiRepository;

        public ApiService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public async Task<bool> ExistsApiKey(string apiKey)
        {
            var apiKeys = await _apiRepository.GetAllAsync(a => a.key == apiKey);
            return apiKeys.Any();
        }

        public async Task<string> GenerateApiKey()
        {
            const int keyLength = 32;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()-_=+";

            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                var apiKey = new StringBuilder(keyLength);
                var randomBytes = new byte[1];

                for (int i = 0; i < keyLength; i++)
                {
                    cryptoProvider.GetBytes(randomBytes);

                    char randomChar = chars[randomBytes[0] % chars.Length];
                    apiKey.Append(randomChar);
                }

                var apiEntity = new ApiKey
                {
                    Id = Guid.NewGuid(),
                    key = apiKey.ToString(),
                };

                await _apiRepository.Create(apiEntity);
                return apiKey.ToString();
            }
        }
    }
}