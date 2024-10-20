﻿using Bank.API.Models;
using BANK.BusinessLayer.Services.Interfaces;
using BANK.DataLayer.Data.Repositories.Interfaces;

namespace BANK.BusinessLayer.Services.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionPrivateService _transactionPrivateService;
        private readonly ITransactionDetailsRepository _transactionDetailsRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionPrivateService transactionPrivateService, ITransactionDetailsRepository transactionDetailsRepository, ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionPrivateService = transactionPrivateService;
            _transactionDetailsRepository = transactionDetailsRepository;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<TransactionDetails>> GetTransactionsByAccountId(Guid accountId)
        {
            return await _transactionDetailsRepository.GetAllAsync(t => t.FromAccountId == accountId || t.ToAccountId == accountId);
        }

        public async Task<TransactionDetails> GetTransactionDetails(Guid transactionId)
        {
            return await _transactionDetailsRepository.Find(transactionId);
        }

        public async Task<TransactionResult> DepositAsync(Guid accountId, string amount)
        {
            var account = (await _accountRepository.GetAllAsync(a => a.Id == accountId)).FirstOrDefault();
            decimal amountValue = decimal.Parse(amount);

            if (account == null)
            {
                return new TransactionResult { Success = false, Message = "errors.account_not_found" };
            }

            if (amountValue <= 0)
            {
                return new TransactionResult { Success = false, Message = "errors.amount_must_be_positive" };
            }

            account.Balance += amountValue;
            await _accountRepository.Update(account);

            var (transaction, transactionDetails) = await _transactionPrivateService.CreateDeposit(account, amountValue);
            await _transactionRepository.Create(transaction);
            await _transactionDetailsRepository.Create(transactionDetails);

            return new TransactionResult { Success = true };
        }

        public async Task<TransactionResult> WithdrawAsync(Guid accountId, string amount)
        {
            var account = (await _accountRepository.GetAllAsync(a => a.Id == accountId)).FirstOrDefault();
            decimal amountValue = decimal.Parse(amount);

            if (amountValue == null)
            {
                return new TransactionResult { Success = false, Message = "errors.account_not_found" };
            }

            if (amountValue <= 0)
            {
                return new TransactionResult { Success = false, Message = "errors.amount_must_be_positive" };
            }

            if (amountValue > account.Balance)
            {
                return new TransactionResult { Success = false, Message = "errors.not_enough_money" };
            }

            account.Balance -= amountValue;
            await _accountRepository.Update(account);

            var (transaction, transactionDetails) = await _transactionPrivateService.CreateWithdraw(account, amountValue);
            await _transactionRepository.Create(transaction);
            await _transactionDetailsRepository.Create(transactionDetails);

            return new TransactionResult { Success = true };
        }

        public async Task<TransactionResult> TransferMoney(string fromAccountId, string toAccountId, decimal amount)
        {
            var fromAccount = await _accountRepository.FindByNumber(fromAccountId);
            var toAccount = await _accountRepository.FindByNumber(toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                return new TransactionResult { Success = false, Message = "errors.account_not_found" };
            }

            if (amount < 0)
            {
                return new TransactionResult { Success = false, Message = "errors.amount_must_be_positive" };
            }

            if (fromAccount.Balance < amount)
            {
                return new TransactionResult { Success = false, Message = "errors.not_enough_money" };
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            await _accountRepository.Update(fromAccount);
            await _accountRepository.Update(toAccount);

            var (transaction, transactionDetails) = await _transactionPrivateService.CreateTransfer(fromAccount, toAccount, amount);
            await _transactionRepository.Create(transaction);
            await _transactionDetailsRepository.Create(transactionDetails);

            return new TransactionResult { Success = true };
        }

        public async Task DeleteTransactionsByAccountId(Guid accountId)
        {
            await _transactionDetailsRepository.RemoveRange(accountId);

            await _transactionRepository.RemoveRange(accountId);

        }

        public async Task<TransactionResult> WithdrawAdminAsync(string accountNumber, string amount)
        {
            var account = (await _accountRepository.GetAllAsync(a => a.AccountNumber == accountNumber)).FirstOrDefault();
            decimal amountValue = decimal.Parse(amount);

            if (amountValue == null)
            {
                return new TransactionResult { Success = false, Message = "errors.account_not_found" };
            }

            if (amountValue <= 0)
            {
                return new TransactionResult { Success = false, Message = "errors.amount_must_be_positive" };
            }

            if (amountValue > account.Balance)
            {
                return new TransactionResult { Success = false, Message = "errors.not_enough_money" };
            }

            account.Balance -= amountValue;
            await _accountRepository.Update(account);


            var (transaction, transactionDetails) = await _transactionPrivateService.CreateWithdraw(account, amountValue);

            await _transactionRepository.Create(transaction);
            await _transactionDetailsRepository.Create(transactionDetails);

            return new TransactionResult { Success = true };
        }

        public async Task<TransactionResult> DepositAdminAsync(string accountNumber, string amount)
        {
            var account = (await _accountRepository.GetAllAsync(a => a.AccountNumber == accountNumber)).FirstOrDefault();
            decimal amountValue = decimal.Parse(amount);

            if (account == null)
            {
                return new TransactionResult { Success = false, Message = "errors.account_not_found" };
            }

            if (amountValue <= 0)
            {
                return new TransactionResult { Success = false, Message = "errors.amount_must_be_positive" };
            }

            account.Balance += amountValue;
            await _accountRepository.Update(account);

            var (transaction, transactionDetails) = await _transactionPrivateService.CreateDeposit(account, amountValue);

            await _transactionRepository.Create(transaction);
            await _transactionDetailsRepository.Create(transactionDetails);

            return new TransactionResult { Success = true };
        }
    }
}





