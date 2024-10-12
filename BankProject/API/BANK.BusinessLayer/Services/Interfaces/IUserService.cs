using Bank.BusinessLayer.Models;
using BANK.DataLayer.Entities;

namespace BANK.BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<bool> ResetPasswordAsync(Guid userId, string oldPassword, string newPassword);

    Task<bool> ResetUsernameAsync(Guid userId, string password, string newUsername);

    Task<bool> DeleteUserByUsernameAsync(string username);

    Task<string> GetUsernameByIdAsync(Guid userId);

    Task<bool> DeleteUserByIdAsync(Guid userId);

    string GenerateJwtToken(User user);

    Task<bool> UserExists(string username);

    string HashPassword(string password);

    Task RegisterUser(Register request);

    Task<User> ValidateUser(Login request);

    Task<User> Find(Guid accountId);
}
