using Bank.API.DTOs;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ApiKeyController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private readonly IApiService _apiService;

        public ApiKeyController(ITransactionService transactionService, IApiService apiService)
        {
            _transactionService = transactionService;
            _apiService = apiService;
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateKey()
        {
            var apiKey = await _apiService.GenerateApiKey();

            return Ok(new { Key = apiKey });
        }

        [HttpPost("use")]
        public async Task<IActionResult> UseKey([FromBody] TransferRequest request)
        {

            var apiKey = Request.Headers.ContainsKey("ApiKey") ? Request.Headers["ApiKey"].ToString() : null;

            var apiExists = await _apiService.ExistsApiKey(apiKey);

            if (!apiExists)
            {
                return Unauthorized(new { message = "Api key doesn't exists" });
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
