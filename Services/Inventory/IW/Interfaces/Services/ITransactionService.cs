using IW.Models.DTOs.TransactionDto;

namespace IW.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionDto> GetTransaction(int id);
        Task<IEnumerable<TransactionDto>> GetTransactions(int offset, int amount);
        Task<IEnumerable<TransactionDto>> GetTransactions(GetTransaction query, int offset, int amount);
        Task UpdateTransaction(int id, UpdateTransaction input);
        Task DeleteTransaction(int id);
        Task CreateTransaction(CreateTransaction input);
        Task CreateTransactions(ICollection<CreateTransaction> inputs);
    }
}
