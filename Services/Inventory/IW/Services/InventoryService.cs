using IW.Common;
using IW.Exceptions.ReadInventoryError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.DTOs.Inventory;
using IW.Models.DTOs.InventoryDto;
using IW.Models.Entities;

namespace IW.Services
{
    public class InventoryService : IInventoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateInventory(CreateInventory input)
        {
            Inventory newInventory = new()
            {
                ProductId=input.ProductId,
                Quantity=input.Quantity,
                Availability=input.Availability,
            };

            InventoryValidator validator = new();
            validator.ValidateAndThrowException(newInventory);

            _unitOfWork.Inventories.Add(newInventory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<InventoryDto> GetInventory(int id)
        {
            var inventory = await InventoryExist(id);
            if (Equals(inventory, null))
            {
                throw new InventoryNotFoundException(id);
            }
            var order = await _unitOfWork.Transactions.GetById(inventory.Id);
            InventoryDto result = new()
            {
                Id = id,
                Availability= inventory.Availability,
                Quantity = inventory.Quantity,
                ProductId = inventory.ProductId
            };
            return result;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories(int offset, int amount)
        {
            var inventories = await _unitOfWork.Inventories.GetAll(offset, amount);
            ICollection<InventoryDto> result = new List<InventoryDto>();
            foreach (var inventory in inventories)
            {
                InventoryDto newInventory = new()
                {
                    Id = inventory.Id,
                    Availability = inventory.Availability,
                    Quantity = inventory.Quantity,
                    ProductId = inventory.ProductId
                };
                result.Add(newInventory);
            }
            return result;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories(GetInvenory query, int offset , int amount )
        {
            var inventories = await _unitOfWork.Inventories.FindByConditionToList(
                p => p.Availability == query.Availability ||
                p.ProductId == query.ProductId
                , offset, amount);
            ICollection<InventoryDto> result = new List<InventoryDto>();
            foreach (var inventory in inventories)
            {
                InventoryDto newInventory = new()
                {
                    Id = inventory.Id,
                    Availability = inventory.Availability,
                    Quantity = inventory.Quantity,
                    ProductId = inventory.ProductId
                };
                result.Add(newInventory);
            }
            return result;
        }

        public async Task UpdateInventory(int id, UpdateInventory model)
        {
            var inventory = await InventoryExist(id);
            if (Equals(inventory, null)) throw new InventoryNotFoundException(id);

            inventory.Availability = model.Availability ?? inventory.Availability;
            inventory.Quantity = model.Quantity ?? inventory.Quantity;

            InventoryValidator validator = new();
            validator.ValidateAndThrowException(inventory);

            _unitOfWork.Inventories.Update(inventory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteInventory(int id)
        {
            var inventory = await InventoryExist(id);
            if (Equals(inventory, null)) throw new InventoryNotFoundException(id);

            _unitOfWork.Inventories.Remove(inventory);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Inventory?> InventoryExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = await _unitOfWork.Inventories.GetById(id);
            return result;
        }
    }
}
