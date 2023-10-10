﻿using IW.Common;
using IW.Exceptions.ReadItemError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDto;
using IW.Models.Entities;
using MapsterMapper;

namespace IW.Services
{
    public class ItemService : IItemService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateItem(int orderId,CreateItem input)
        {
            OrderItem newProduct = _mapper.Map<OrderItem>(input);
            newProduct.OrderId = orderId;

            ItemValidator validator = new();
            validator.ValidateAndThrowException(newProduct);

            _unitOfWork.Items.Add(newProduct);
            await _unitOfWork.CompleteAsync();
        }

        public async Task CreateItems(int orderId,IEnumerable<CreateItem> inputs)
        {
            ICollection<OrderItem> items=new List<OrderItem>();
            ItemValidator validator = new();

            foreach (var input in inputs)
            {
                OrderItem newProduct = _mapper.Map<OrderItem>(input);
                newProduct.OrderId = orderId;

                validator.ValidateAndThrowException(newProduct);
                items.Add(newProduct);
            }

            _unitOfWork.Items.AddRange(items);
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
