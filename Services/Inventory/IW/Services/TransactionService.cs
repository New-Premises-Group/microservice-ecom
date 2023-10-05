﻿using IW.Exceptions.ReadTransactionError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.TransactionDto;
using IW.Models.Entities;

namespace IW.Services
{
    public class TransactionService : ITransactionService
    {
        public readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateTransaction(CreateTransaction input)
        {
            var inventory = await _unitOfWork.Inventories.GetById(input.InventoryId);
            Transaction newTransaction = new()
            {
                InventoryId = input.InventoryId,
                Note = input.Note,
                Quantity = input.Quantity,
                Type = input.Type,
                Date= DateTime.Now,
                Inventory= inventory
            };

            TransactionValidator validator = new();
            validator.ValidateAndThrowException(newTransaction);

            _unitOfWork.Transactions.Add(newTransaction);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteTransaction(int id)
        {
            var transaction = await TransactionExist(id);
            if (Equals(transaction, null)) throw new TransactionNotFoundException(id);

            _unitOfWork.Transactions.Remove(transaction);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions(int offset,int amount )
        {
            var transactions = await _unitOfWork.Transactions.GetAll(offset,amount);
            ICollection<TransactionDto> result = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                TransactionDto item = new()
                {
                    Id = transaction.Id,
                    InventoryId = transaction.InventoryId,
                    Date= transaction.Date,
                    Note = transaction.Note,
                    Quantity= transaction.Quantity,
                    Type = transaction.Type,
                    
                };
                result.Add(item);
            }
            return result;
        }

        public async Task<TransactionDto> GetTransaction(int id)
        {
            var transaction = await TransactionExist(id);
            if (Equals(transaction, null))
            {
                throw new TransactionNotFoundException(id);
            }
            TransactionDto result = new()
            {
                Id = id,
                InventoryId = transaction.InventoryId,
                Date = transaction.Date,
                Note = transaction.Note,
                Quantity = transaction.Quantity,
                Type = transaction.Type,
            };
            return result;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions(GetTransaction query, int offset, int amount)
        {
            var transactions = await _unitOfWork.Transactions.FindByConditionToList(
                o => o.InventoryId==query.InventoryId ||
                o.Date==query.Date ||
                o.Type ==query.Type
                , offset, amount);

            ICollection<TransactionDto> result = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                TransactionDto item = new()
                {
                    Id = transaction.Id,
                    InventoryId = transaction.InventoryId,
                    Date = transaction.Date,
                    Note = transaction.Note,
                    Quantity = transaction.Quantity,
                    Type = transaction.Type,

                };
                result.Add(item);
            }
            return result;
        }

        public async Task UpdateTransaction(int id, UpdateTransaction input)
        {
            var transaction = await TransactionExist(id);
            if (Equals(transaction, null)) throw new TransactionNotFoundException(id);

            transaction.InventoryId = input.InventoryId;
            transaction.Note = input.Note ?? transaction.Note;

            TransactionValidator validator = new ();
            validator.ValidateAndThrowException(transaction);

            _unitOfWork.Transactions.Update(transaction);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Transaction?> TransactionExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var Order = await _unitOfWork.Transactions.GetById(id);
            return Order;
        }
    }
}