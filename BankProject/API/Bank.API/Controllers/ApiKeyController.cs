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

        private readonly IBusService _busService;

        public ApiKeyController(ITransactionService transactionService, IApiService apiService, IBusService busService)
        {
            _transactionService = transactionService;
            _apiService = apiService;
            _busService = busService;
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateKey()
        {
            var apiKey = await _apiService.GenerateApiKey();

            return Ok(new { Key = apiKey });
        }

        [HttpPost("use")]
        public async Task<IActionResult> UseKey([FromBody] ShopRequest request)
        {
            string status = "success";
            var apiKey = Request.Headers.ContainsKey("ApiKey") ? Request.Headers["ApiKey"].ToString() : null;

            var apiExists = await _apiService.ExistsApiKey(apiKey);

            if (!apiExists)
            {
                status = "Api key doesn't exists";
                await SendMessageAsync(request, status);
                return Unauthorized(new { message = "Api key doesn't exists" });
            }

            if (request.ToAccount == request.FromAccount)
            {
                status = "Operation Denied";
                await SendMessageAsync(request, status);
                return BadRequest(new { message = "errors.operation_denied" });
            }

            var result = await _transactionService.TransferMoney(request.FromAccount, request.ToAccount, Decimal.Parse(request.Amount));

            if (!result.Success)
            {
                status = $"{result.Message}";
                await SendMessageAsync(request, status);
                return BadRequest(new { message = result.Message });

            }

            await SendMessageAsync(request, status);
            return Ok(new { status = true, message = "transaction.transfer_success" });
        }

        private async Task SendMessageAsync(ShopRequest request, string status)
        {
            await _busService.messageAsync(
                request.FromAccount,
                request.ToAccount,
                request.Amount,
                request.Email,
                request.OrderId,
                status
            );
        }
    }
}
