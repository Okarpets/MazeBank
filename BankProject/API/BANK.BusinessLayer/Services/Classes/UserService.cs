using Bank.BusinessLayer.Models;
using BANK.BusinessLayer.Services.Interfaces;
using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BANK.BusinessLayer.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(ITransactionRepository transactionRepository, IAccountService accountService, IUserRepository userRepository, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> Find(Guid accountId)
        {
            return await _userRepository.Find(accountId);
        }

        public string HashPassword(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        public async Task RegisterUser(Register request)
        {
            if (await _userRepository.Exists(request.Username))
            {
                throw new InvalidOperationException("errors.user_exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Username = request.Username,
                PasswordHash = HashPassword(request.Password),
                Role = request.Role
            };

            await _userRepository.Create(user);
            await _userRepository.SaveChangesAsync();

            await _accountService.CreateAccount(user.Id);
        }

        public async Task<User> ValidateUser(Login request)
        {
            var user = await _userRepository.FindByUsername(request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash).Result)
            {
                return null;
            }
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return (await _userRepository.GetAllAsync(u => u.Username == username)).Any();
        }

        public async Task<bool> VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            var parts = storedPasswordHash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return storedHash == enteredHash;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> DeleteUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserWithTransactions(userId);
            if (user == null)
            {
                return false;
            }

            var accounts = await _accountService.GetAllUserAccountsById(userId);

            foreach (var account in accounts)
            {
                await _accountService.DeleteAccountByNumber(account.AccountNumber);
            }

            await _userRepository.Delete(userId);

            await _userRepository.SaveChangesAsync();

            return true;
        }


        public async Task<bool> ResetPasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.Find(userId);
            if (user == null || !await VerifyPassword(oldPassword, user.PasswordHash))
            {
                return false;
            }

            user.PasswordHash = HashPassword(newPassword);
            await _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ResetUsernameAsync(Guid userId, string password, string newUsername)
        {
            var user = await _userRepository.Find(userId);
            if (user == null || !await VerifyPassword(password, user.PasswordHash))
            {
                return false;
            }
            user.Username = newUsername;
            await _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetUsernameByIdAsync(Guid userId)
        {
            var user = await _userRepository.Find(userId);
            return user?.Username;
        }

        public async Task<bool> DeleteUserByUsernameAsync(string username)
        {
            var user = await _userRepository.FindByUsername(username);
            if (user == null)
            {
                return false;
            }

            var accounts = await _accountService.GetAllUserAccountsById(user.Id);

            foreach (var account in accounts)
            {
                await _accountService.DeleteAccountByNumber(account.AccountNumber);
            }

            return await _userRepository.DeleteByUsername(username);
        }
    }
}
