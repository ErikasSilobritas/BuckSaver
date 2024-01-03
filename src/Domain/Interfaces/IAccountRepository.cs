using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountEntity?> GetAccount(int id);
        Task<IEnumerable<AccountEntity?>> GetAccounts(int userId);
        Task<decimal?> GetAccountBalance(int accountId);
        Task<AccountEntity> CreateAccount(AccountEntity account);
        Task UpdateBalance(AccountEntity accountUpdate);
    }
}
