using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnection _connection;

        public TransactionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<TransactionEntity?> AppendTransactions(TransactionEntity transaction)
        {
            string sql = "INSERT INTO transactions (\"accountId\", amount, type, fees) VALUES (@AccountId, @Amount, @Type, @Fees) RETURNING *";
            var queryArguments = new
            {
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Fees = transaction.Fees
            };

            return await _connection.QuerySingleOrDefaultAsync<TransactionEntity>(sql, queryArguments);
        }

        public async Task<IEnumerable<TransactionEntity?>> Get(int accountId)
        {

            string sql = "SELECT * FROM transactions WHERE \"accountId\" = @AccountId AND \"isDeleted\" = false";
            return await _connection.QueryAsync<TransactionEntity?>(sql, new { AccountId = accountId });
        }

        public async Task<IEnumerable<TransactionEntity?>> GetTransactionsByUserId(int userId)
        {
            string sql = "SELECT transactions.id, transactions.\"accountId\", transactions.type, transactions.amount, transactions.fees FROM transactions JOIN accounts ON transactions.\"accountId\" = accounts.id WHERE accounts.\"userId\" = @UserId AND transactions.\"isDeleted\" = false";
            var queryArguments = new
            {
                UserId = userId
            };
            return await _connection.QueryAsync<TransactionEntity?>(sql, queryArguments);
        }
    }
}
