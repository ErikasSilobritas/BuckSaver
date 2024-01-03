using Domain.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;

        }

        public async Task<List<GetTransactions>> GetTransactionsByUserId(int userId)
        {
            var user = await _userRepository.GetUser(userId);

            if (user is null)
            {
                throw new UserNotFoundException();
            }

            IEnumerable<AccountEntity?> accounts = await _accountRepository.GetAccounts(userId);

            if (!accounts.Any())
            {
                throw new AccountNotFoundException();
            }

            IEnumerable<TransactionEntity?> requestedTransactions = await _transactionRepository.GetTransactionsByUserId(userId);
            List<TransactionEntity?> transactionList = requestedTransactions.ToList();

            List<GetTransactions> transactions = transactionList.Select(t => new GetTransactions
            {
                AccountId = t.AccountId,
                Type = t.Type,
                Amount = t.Amount,
                Fees = t.Fees ?? 0m
            }).ToList();

            return transactions;

        }

        //public async Task<List<GetTransactions>> Get (int userId)
        //{
        //    IEnumerable<AccountEntity?> accounts = await _accountRepository.GetAccounts(userId);

        //    if (accounts is null)
        //    {
        //        throw new AccountNotFoundException();
        //    }

        //    List<AccountEntity> accountList = accounts.Where(account => account != null).Select(account => account!).ToList();
        //    List<TransactionEntity?> transactionList = new List<TransactionEntity?>();

        //    foreach (var account in accountList) 
        //    {
        //        var accountTransactions = await _transactionRepository.Get(account.Id);
        //        if (accountTransactions != null)
        //        {
        //            transactionList.AddRange(accountTransactions);
        //        }
        //    }

        //    List<GetTransactions> transactions = transactionList.Select(t => new GetTransactions
        //    {
        //        AccountId = t.AccountId,
        //        Type = t.Type,
        //        Amount = t.Amount,
        //        Fees = t.Fees ?? 0m
        //    }).ToList();

        //    return transactions;
        //}

        public async Task<TopUp> TopUp(TopUp topUp)
        {
            if (topUp.Amount < 0.01m)
            {
                throw new IncorrectTransferAmountException();
            }

            decimal? currentBalance = await _accountRepository.GetAccountBalance(topUp.AccountId) ?? 0m;

            AccountEntity accountUpdate = new AccountEntity
            {
                Id = topUp.AccountId,
                Balance = topUp.Amount + currentBalance
            };

            await _accountRepository.UpdateBalance(accountUpdate);

            TransactionEntity transaction = new TransactionEntity
            {
                AccountId = topUp.AccountId,
                Amount = topUp.Amount,
                Type = "TopUp",
                Fees = 0m
            };

            var requestedTransaction = await _transactionRepository.AppendTransactions(transaction);
            TopUp confirmedTransaction = new TopUp
            {
                AccountId = requestedTransaction.AccountId,
                Amount = requestedTransaction.Amount
            };

            return confirmedTransaction;
        }

        public async Task Transfer(Transfer transfer)
        {
            if (transfer.Amount < 0.01m)
            {
                throw new IncorrectTransferAmountException();
            }
            bool transferToOwnAccount = false;
            decimal fee = 1m;

            int senderId = (await _accountRepository.GetAccount(transfer.FromAccountId))!.UserId;
            int receiverId = (await _accountRepository.GetAccount(transfer.ToAccountId))!.UserId;
            
            if (senderId == receiverId)
            {
                transferToOwnAccount = true;
            }

            fee = transferToOwnAccount ? 0m : fee;

            decimal? currentSenderBalance = await _accountRepository.GetAccountBalance(transfer.FromAccountId) ?? 0m;
            decimal? currentReceiverBalance = await _accountRepository.GetAccountBalance(transfer.ToAccountId) ?? 0m;

            if (currentSenderBalance < transfer.Amount + fee)
            {
                throw new InsufficientFundsException();
            }

            AccountEntity reduceBalance = new AccountEntity
            {
                Id = transfer.FromAccountId,
                Balance = currentSenderBalance - transfer.Amount - fee
            };
            await _accountRepository.UpdateBalance(reduceBalance);

            AccountEntity increaseBalance = new AccountEntity
            {
                Id = transfer.ToAccountId,
                Balance = currentReceiverBalance + transfer.Amount
            };
            await _accountRepository.UpdateBalance(increaseBalance);

            TransactionEntity senderTransaction = new TransactionEntity
            {
                AccountId = transfer.FromAccountId,
                Amount = -transfer.Amount,
                Type = "FundsSent",
                Fees = -fee
            };
            await _transactionRepository.AppendTransactions(senderTransaction);

            TransactionEntity receiverTransaction = new TransactionEntity
            {
                AccountId = transfer.ToAccountId,
                Amount = transfer.Amount,
                Type = "FundsReceived",
                Fees = 0m
            };
            await _transactionRepository.AppendTransactions(receiverTransaction);

        }
    }
}
