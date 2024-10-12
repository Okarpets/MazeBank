using Bank.API.DTOs;
using BANK.API.Models;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopUsageController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ShopSettings _shopSettings;

        public ShopUsageController(ITransactionService transactionService, IOptions<ShopSettings> shopSettings)
        {
            _transactionService = transactionService;
            _shopSettings = shopSettings.Value;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> DoTransfer([FromBody] TransferRequest request)
        {
            var shopIdClaim = User.FindFirst("shopId")?.Value;
            var urlClaim = User.FindFirst("url")?.Value;

            if (urlClaim != _shopSettings.ShopUrl && shopIdClaim != _shopSettings.ShopId)
            {
                return BadRequest(new { status = false });
            }

            if (request.ToAccount == request.FromAccount)
            {
                return BadRequest(new { message = "errors.operation_denied" });
            }

            var result = await _transactionService.TransferMoney(request.FromAccount, request.ToAccount, Decimal.Parse(request.Amount));

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { status = true, message = "transaction.transfer_success" });
        }
    }
}



