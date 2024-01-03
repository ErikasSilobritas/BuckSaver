using Domain.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;

        }

        public async Task<CreateAccount> CreateAccount(CreateAccount createAccount)
        {
            if (await _userRepository.GetUser(createAccount.UserId) is null)
            {
                throw new UserDoesNotExistException();
            }
            int accountCount = await GetAccountCount(createAccount.UserId);

            if (accountCount >= 2)
            {
                throw new AccountNumberExceededException();
            }

            if (createAccount.Type.ToLower() != "checking" && createAccount.Type.ToLower() != "savings")
            {
                throw new InvalidAccountTypeException();
            }

            AccountEntity account = new AccountEntity
            {
                UserId = createAccount.UserId,
                Type = createAccount.Type.ToLower()
            };

            var requestedAccount = await _accountRepository.CreateAccount(account);

            CreateAccount createdAccount = new CreateAccount
            {
                UserId = requestedAccount.UserId,
                Type = requestedAccount.Type
            };
            return createdAccount;
        }

        public async Task<int> GetAccountCount(int userId)
        {
            var accounts = await _accountRepository.GetAccounts(userId);
            int accountCount = accounts.Count();
            return accountCount;
        }
    }


}
