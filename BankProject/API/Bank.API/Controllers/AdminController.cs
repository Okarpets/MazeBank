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

    public class AdminController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        public AdminController(ITransactionService transactionService, IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DoDepositByNumber([FromBody] AdminTransactionRequest request)
        {
            var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (userRoleClaim?.Value != "Admin")
            {
                return Unauthorized(new { message = "errors.unauthorized" });
            }

            var result = await _transactionService.DepositAdminAsync(request.Account, request.Amount);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> DoWithdrawByNumber([FromBody] AdminTransactionRequest request)
        {
            var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (userRoleClaim?.Value != "Admin")
            {
                return Unauthorized(new { message = "errors.unauthorized" });
            }

            var result = await _transactionService.WithdrawAdminAsync(request.Account, request.Amount);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok();
        }

        [HttpDelete("delete/{username}")]
        public async Task<IActionResult> DeleteUserByName(string username)
        {
            var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (userRoleClaim?.Value != "Admin")
            {
                return Unauthorized(new { message = "errors.unauthorized" });
            }

            var result = await _userService.DeleteUserByUsernameAsync(username);
            if (!result)
            {
                return BadRequest(new { message = "accounts.delete_error" });
            }

            return Ok(new { status = true, message = "accounts.delete_success" });
        }
    }
}
