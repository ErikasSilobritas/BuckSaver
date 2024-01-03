using Dapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TransactionEntity?>> Get (int accountId)
        {
            
            string sql = "SELECT * FROM transactions WHERE \"accountId\" = @AccountId AND \"isDeleted\" = false";
            return await _connection.QueryAsync<TransactionEntity?>(sql, new {AccountId =  accountId});
        }

        public async Task<IEnumerable<TransactionEntity?>> GetTransactionsByUserId(int userId)
        {
            string sql = "SELECT * FROM transactions JOIN accounts ON transactions.\"accountId\" = accounts.id WHERE accounts.\"userId\" = @UserId AND \"isDeleted\" = false";
            var queryArguments = new
            {
                UserId = userId
            };
            return await _connection.QueryAsync<TransactionEntity?>(sql, queryArguments);
        }
    }
}
