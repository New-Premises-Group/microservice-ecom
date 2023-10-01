using IW.Common;
using IW.Exceptions.ReadItemError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDto;
using IW.Models.Entities;

namespace IW.Services
{
    public class ItemService : IItemService
    {
        public readonly IUnitOfWork _unitOfWork;
        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateItem(CreateItem input)
        {
            var item = await _unitOfWork.Items.FindByCondition(u => u.Name == input.Name);
            Order order = await _unitOfWork.Orders.GetById(input.OrderId);

            OrderItem newProduct = new()
            {
                Name = input.Name,
                Order= order,
                OrderId= input.OrderId,
                Price= input.Price,
                ProductId= input.ProductId,
                Quantity= input.Quantity,
                SKU= input.SKU,
                Subtotal= input.Price*input.Quantity
            };

            ItemValidator validator = new();
            validator.ValidateAndThrowException(newProduct);

            _unitOfWork.Items.Add(newProduct);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ItemDto> GetItem(int id)
        {
            var item = await ItemExist(id);
            if (Equals(item, null))
            {
                throw new ItemNotFoundException(id);
            }
            var order = await _unitOfWork.Orders.GetById(item.Id);
            ItemDto result = new()
            {
                Id = id,
                Name = item.Name,
                Order = order,
                OrderId = item.OrderId,
                Price = item.Price,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                SKU = item.SKU,
                Subtotal = item.Subtotal
            };
            return result;
        }

        public async Task<IEnumerable<ItemDto>> GetItems(int offset, int amount)
        {
            var items = await _unitOfWork.Items.GetAll(offset, amount);
            ICollection<ItemDto> result = new List<ItemDto>();
            foreach (var item in items)
            {
                ItemDto newItem = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Order = item.Order,
                    OrderId = item.OrderId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SKU = item.SKU,
                    Subtotal = item.Subtotal
                };
                result.Add(newItem);
            }
            return result;
        }

        public async Task<IEnumerable<ItemDto>> GetItems(GetItem query, int offset , int amount )
        {
            var items = await _unitOfWork.Items.FindByConditionToList(
                p => p.Name == query.Name ||
                p.OrderId == query.OrderId ||
                p.Price == query.Price ||
                p.ProductId== query.ProductId
                , offset, amount);
            ICollection<ItemDto> result = new List<ItemDto>();
            foreach (var item in items)
            {
                ItemDto newItem = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Order = item.Order,
                    OrderId = item.OrderId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SKU = item.SKU,
                    Subtotal = item.Subtotal
                };
                result.Add(newItem);
            }
            return result;
        }

        public async Task UpdateItem(int id, UpdateItem model)
        {
            var item = await ItemExist(id);
            if (Equals(item, null)) throw new ItemNotFoundException(id);

            item.Name = model.Name ?? item.Name;
            item.Price = model.Price ??item.Price;
            item.ProductId = model.ProductId ?? item.ProductId;
            item.Quantity = model.Quantity ?? item.Quantity;
            item.SKU = model.SKU;

            ItemValidator validator = new();
            validator.ValidateAndThrowException(item);

            _unitOfWork.Items.Update(item);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteItem(int id)
        {
            var item = await ItemExist(id);
            if (Equals(item, null)) throw new ItemNotFoundException(id);

            _unitOfWork.Items.Remove(item);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<OrderItem?> ItemExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = await _unitOfWork.Items.GetById(id);
            return result;
        }
    }
}
