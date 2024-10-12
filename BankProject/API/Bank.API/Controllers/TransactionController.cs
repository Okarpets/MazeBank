using Bank.API.DTOs;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransactionController(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        [HttpGet("detail/{operationId}")]
        public async Task<IActionResult> GetOperationDetail(Guid operationId)
        {
            var transaction = await _transactionService.GetTransactionDetails(operationId);
            if (transaction == null)
            {
                return NotFound(new { message = "errors.transaction_not_found" });
            }

            var response = new
            {
                id = transaction.Id,
                description = transaction.Description,
                amount = transaction.Amount
            };

            return Ok(response);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetTransactionsForAccount(Guid accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountId(accountId);
            if (transactions == null || !transactions.Any())
            {
                return NotFound(new { message = "errors.transactions_not_found" });
            }

            var result = transactions.Select(t => new
            {
                t.Id,
                t.Description
            });

            return Ok(result);
        }

        [HttpGet("balance/{accountId}")]
        public async Task<IActionResult> GetBalanceByNumber(Guid accountId)
        {
            var account = await _accountService.GetUserAccountById(accountId);
            if (account == null)
            {
                return NotFound(new { message = "errors.account_not_found" });
            }

            var balance = account.Balance;

            return Ok(balance);
        }

        [HttpDelete("delete/{accountNumber}")]
        public async Task<IActionResult> DeleteAccountByNumber(string accountNumber)
        {
            var account = await _accountService.GetUserAccountByNumber(accountNumber);
            if (account == null)
            {
                return NotFound(new { message = "errors.account_not_found" });
            }

            await _transactionService.DeleteTransactionsByAccountId(account.Id);

            await _accountService.DeleteAccountById(account.Id);

            return Ok(new { status = true, message = "accounts.delete_success" });
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DoDeposit([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.DepositAsync(request.Account, request.Amount);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> DoWithdraw([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.WithdrawAsync(request.Account, request.Amount);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> DoTransfer([FromBody] TransferRequest request)
        {
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



