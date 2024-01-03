using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionEntity?> AppendTransactions(TransactionEntity transaction);
        Task<IEnumerable<TransactionEntity?>> Get(int accountId);
        Task<IEnumerable<TransactionEntity?>> GetTransactionsByUserId(int userId);
    }
}
