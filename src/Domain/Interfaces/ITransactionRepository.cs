using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionEntity?> AppendTransactions(TransactionEntity transaction);
        Task<IEnumerable<TransactionEntity?>> Get(int accountId);
        Task<IEnumerable<TransactionEntity?>> GetTransactionsByUserId(int userId);
    }
}
