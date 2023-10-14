using IW.Common;
using IW.Exceptions.ReadInventoryError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.MessageBroker;
using IW.Models.DTOs;
using IW.Models.DTOs.Inventory;
using IW.Models.DTOs.InventoryDto;
using IW.Models.DTOs.TransactionDto;
using IW.Models.Entities;
using Mapster;
using MapsterMapper;

namespace IW.Services
{
    public class InventoryService : IInventoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ITransactionService _transactionService;
        public InventoryService(IUnitOfWork unitOfWork,IMapper mapper,ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionService = transactionService;

            TypeAdapterConfig<InventoryDto, CreateTransaction>
                .NewConfig()
                .Map(dest => dest.Date, _ => DateTime.Now.ToUniversalTime());

            TypeAdapterConfig<Inventory, CreateTransaction>
                .NewConfig()
                .Map(dest => dest.Date, _ => DateTime.Now.ToUniversalTime());
        }

        public async Task CreateInventory(CreateInventory input)
        {
            Inventory newInventory = _mapper.Map<Inventory>(input);

            InventoryValidator validator = new();
            validator.ValidateAndThrowException(newInventory);

            _unitOfWork.Inventories.Add(newInventory);
            await _unitOfWork.CompleteAsync();
            CreateTransaction transactionDto = _mapper.Map<CreateTransaction>(newInventory);
            transactionDto.Type = TRANSACTION_TYPE.Restock;
            transactionDto.Inventories = newInventory;
            transactionDto.InventoryId = newInventory.Id;
            await _transactionService.CreateTransaction(transactionDto);
        }

        public async Task<InventoryDto> GetInventory(int id)
        {
            var inventory = await InventoryExist(id);
            if (Equals(inventory, null))
            {
                throw new InventoryNotFoundException(id);
            }
            InventoryDto result = _mapper.Map<InventoryDto>(inventory);
            return result;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories(int offset, int amount)
        {
            var inventories = await _unitOfWork.Inventories.GetAll(offset, amount);
            ICollection<InventoryDto> result = _mapper.Map<List<InventoryDto>>(inventories);
            return result;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories(GetInvenory query, int offset , int amount )
        {
            var inventories = await _unitOfWork.Inventories.FindByConditionToList(
                p => p.Availability == query.Availability ||
                p.ProductId == query.ProductId
                , offset, amount);
            ICollection<InventoryDto> result = _mapper.Map<List<InventoryDto>>(inventories);
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

            CreateTransaction transactionDto = _mapper.Map<CreateTransaction>(inventory);
            transactionDto.Type = TRANSACTION_TYPE.Adjustment;
            await _transactionService.CreateTransaction(transactionDto);
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

        public async Task UpdateInventories(ICollection<InventoryDto> inventories)
        {
            _unitOfWork.Inventories.UpdateRange(inventories.Adapt<ICollection<Inventory>>());
            await _unitOfWork.CompleteAsync();

            TypeAdapterConfig<InventoryDto,CreateTransaction>
                .NewConfig()
                .Map(dest=>dest.Type,_=>TRANSACTION_TYPE.Adjustment);

            ICollection<CreateTransaction> transactionDto = _mapper.Map<ICollection<CreateTransaction>>(inventories);
            await _transactionService.CreateTransactions(transactionDto);
        }

        public async Task UpdateStocks(ICollection<InventoryDto> inventoryDto,TRANSACTION_TYPE type)
        {
            foreach (var item in inventoryDto)
            {
                _unitOfWork.Inventories.UpdateQuantity(item.Adapt<Inventory>());
            }
            await _unitOfWork.CompleteAsync();

            TypeAdapterConfig<InventoryDto, CreateTransaction>
                .NewConfig()
                .Map(dest => dest.Type, _ => type);

            ICollection<CreateTransaction> transactionDto = _mapper.Map<ICollection<CreateTransaction>>(inventoryDto);
            await _transactionService.CreateTransactions(transactionDto);
        }
    }
}
