using Bank.API.DTOs;
using BANK.API.DTOs;
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

            var fromAccountNumber = await _accountService.GetAccountNumberByIdAsync(transaction.FromAccountId.Value);
            var toAccountNumber = await _accountService.GetAccountNumberByIdAsync(transaction.ToAccountId.Value);

            var response = new
            {
                transaction.Id,
                transaction.Amount,
                TransactionDate = transaction.TransactionDate.ToString("dd/MM/yyyy - HH:mm:ss"),
                FromAccountNumber = await _accountService.GetAccountNumberByIdAsync(transaction.FromAccountId.Value),
                ToAccountNumber = await _accountService.GetAccountNumberByIdAsync(transaction.ToAccountId.Value),
                transaction.TransactionType,
            };

            return Ok(response);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetTransactionsForAccount([FromBody] GetTransactionRequest request)
        {
            var transactions = await _transactionService.GetTransactionsByAccountId(request.accountId);
            if (transactions == null || !transactions.Any())
            {
                return NotFound(new { message = "errors.transactions_not_found" });
            }

            switch (request.filter)
            {
                case 1:
                    transactions = transactions.Where(t => t.TransactionType == 1).ToList();
                    break;
                case 2:
                    transactions = transactions.Where(t => t.TransactionType == 2).ToList();
                    break;
                case 3:
                    transactions = transactions.Where(t => t.TransactionType == 3).ToList();
                    break;
                case 0:
                    break;
                default:
                    return BadRequest(new { message = "Invalid transaction type filter." });
            }

            var results = new List<object>();

            foreach (var transaction in transactions)
            {
                var fromAccountNumber = await _accountService.GetAccountNumberByIdAsync(transaction.FromAccountId.Value);
                var toAccountNumber = await _accountService.GetAccountNumberByIdAsync(transaction.ToAccountId.Value);

                results.Add(new
                {
                    transaction.Id,
                    transaction.Description,
                    TransactionDate = transaction.TransactionDate.ToString("dd/MM/yyyy - HH:mm:ss"),
                    FromAccountNumber = fromAccountNumber,
                    ToAccountNumber = toAccountNumber,
                    transaction.Amount,
                    transaction.TransactionType
                });
            }

            return Ok(results);
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



