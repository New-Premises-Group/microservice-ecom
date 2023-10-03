using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.TransactionDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class TransactionQuery
    {
        public async Task<IEnumerable<TransactionDto>> GetTransactions([Service] ITransactionService transactionService, int offset = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await transactionService.GetTransactions(offset,amount);
            return results;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions(GetTransaction query,[Service] ITransactionService transactionService, int offset = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await transactionService.GetTransactions(query, offset, amount);
            return results;
        }

        public async Task<TransactionDto> GetTransaction(int id, [Service] ITransactionService transactionService)
        {
            var result = await transactionService.GetTransaction(id);
            return result;
        }
    }
}
