using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IAccountService accountService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid accountId)
        {
            var account = await _accountService.GetAllUserAccountsById(accountId);
            if (account == null)
            {
                return Forbid();
            }
            return Ok(account);
        }

        [HttpGet("create/{userId}")]
        public async Task<IActionResult> CreateAccount(Guid userId)
        {
            var account = await _accountService.CreateAccount(userId);
            return Ok(account);
        }
    }
}
