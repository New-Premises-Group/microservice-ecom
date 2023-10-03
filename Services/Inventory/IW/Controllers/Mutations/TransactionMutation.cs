using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateTransactionError;
using IW.Interfaces.Services;
using IW.Models.DTOs.TransactionDto;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class TransactionMutation
    {
        [Error(typeof(CreateTransactionErrorFactory))]
        public async Task<TransactionCreatedPayload> CreateTransaction(CreateTransaction input, [Service] ITransactionService transactionService)
        {
            await transactionService.CreateTransaction(input);
            var payload = new TransactionCreatedPayload()
            {
                Message = "Transaction successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateTransactionErrorFactory))]
        public async Task<TransactionCreatedPayload> UpdateTransaction(int id, UpdateTransaction input, [Service] ITransactionService transactionService)
        {
            await transactionService.UpdateTransaction(id, input);
            var payload = new TransactionCreatedPayload()
            {
                Message = "Transaction successfully updated"
            };
            return payload;
        }
    }
}
