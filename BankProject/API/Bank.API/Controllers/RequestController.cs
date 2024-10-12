using Bank.API.DTOs;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public RequestController(IUserService userService, IAccountService accountService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUserById(Guid userId)
        {
            var success = await _userService.DeleteUserByIdAsync(userId);
            if (!success)
            {
                return NotFound(new { message = "errors.user_not_found" });
            }

            return Ok(new { status = true, message = "accounts.delete_success" });
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetUsernameFromApi()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest(new { message = "errors.invalid_user_id" });
            }

            var username = await _userService.GetUsernameByIdAsync(userId);
            if (username == null)
            {
                return NotFound(new { message = "errors.user_not_found" });
            }

            return Ok(username);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> PasswordReset([FromBody] UpdateUserPasswordRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var userId = Guid.Parse(userIdClaim?.Value);

            var success = await _userService.ResetPasswordAsync(userId, request.OldPassword, request.NewPassword);
            Console.WriteLine(success);
            if (!success)
            {
                return Unauthorized(new { message = "errors.invalid_password" });
            }

            return Ok();
        }

        [HttpPost("reset-username")]
        public async Task<IActionResult> UsernameReset([FromBody] UpdateUsernameRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdClaim?.Value);

            var success = await _userService.ResetUsernameAsync(userId, request.Password, request.Username);
            if (!success)
            {
                return Unauthorized(new { message = "errors.invalid_password" });
            }

            return Ok();
        }

        [HttpGet("account-number")]
        public async Task<IActionResult> GetAccountNumberById([FromQuery] Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                return BadRequest(new { message = "errors.invalid_account_id" });
            }

            var accountNumber = await _accountService.GetAccountNumberByIdAsync(accountId);
            if (accountNumber == null)
            {
                return NotFound(new { message = "errors.account_not_found" });
            }

            return Ok(accountNumber);
        }
    }
}
