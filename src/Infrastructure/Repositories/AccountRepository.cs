using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{

    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _connection;

        public AccountRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<AccountEntity?> GetAccount(int id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @Id AND \"isDeleted\" = false";
            var queryArguments = new
            {
                Id = id
            };
            return _connection.QuerySingleOrDefault<AccountEntity?>(sql, queryArguments);
        }
        public async Task<IEnumerable<AccountEntity?>> GetAccounts(int userId)
        {
            string sql = "SELECT * FROM accounts WHERE \"userId\" = @UserId AND \"isDeleted\" = false";
            var queryArguments = new
            {
                UserId = userId
            };

            return await _connection.QueryAsync<AccountEntity?>(sql, queryArguments);
        }

        public async Task<decimal?> GetAccountBalance(int accountId)
        {
            string sql = "SELECT balance FROM accounts WHERE id = @AccountId";
            var queryArguments = new
            {
                AccountId = accountId
            };

            decimal? balance = _connection.QuerySingleOrDefault<decimal?>(sql, queryArguments);
            return balance;
        }
        public async Task<AccountEntity> CreateAccount(AccountEntity account)
        {
            string sql = "INSERT INTO accounts (\"userId\", type) VALUES (@UserId, @AccountType) RETURNING *";
            var queryArguments = new
            {
                UserId = account.UserId,
                AccountType = account.Type
            };

            return _connection.QuerySingle<AccountEntity>(sql, queryArguments);
        }

        public async Task UpdateBalance(AccountEntity accountUpdate)
        {
            string sql = "UPDATE accounts SET balance = @Amount WHERE id = @Id";
            var queryArguments = new
            {
                Id = accountUpdate.Id,
                Amount = accountUpdate.Balance
            };

            _connection.Execute(sql, queryArguments);

        }
    }
}
