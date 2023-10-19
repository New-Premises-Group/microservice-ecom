using IW.Exceptions.ReadCartItemError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.CartItemDto;
using IW.Models.Entities;
using MapsterMapper;

namespace IW.Services
{
    public class CartItemService : ICartItemService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateCartItem(CreateCartItem input)
        {
            var cart = await _unitOfWork.Carts.GetById(input.Id);
            CartItem newCartItem = _mapper.Map<CartItem>(cart);

            CartItemValidator validator = new();
            validator.ValidateAndThrowException(newCartItem);

            _unitOfWork.CartItems.Add(newCartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCartItem(int id)
        {
            var cartItem = await CartItemExist(id);
            if (Equals(cartItem, null)) throw new CartItemNotFoundException(id);

            _unitOfWork.CartItems.Remove(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItems(int offset, int amount)
        {
            var cartItems = await _unitOfWork.CartItems.GetAll(offset, amount);
            ICollection<CartItemDto> result = _mapper.Map<List<CartItemDto>>(cartItems);
            return result;
        }

        public async Task<CartItemDto> GetCartItem(int id)
        {
            var cartItem = await CartItemExist(id);
            if (Equals(cartItem, null))
            {
                throw new CartItemNotFoundException(id);
            }
            CartItemDto result = _mapper.Map<CartItemDto>(cartItem);
            return result;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItems(GetCartItem query, int offset, int amount)
        {
            var cartItems = await _unitOfWork.CartItems.FindByConditionToList(
                c => c.Id == query.Id
                , offset, amount);

            ICollection<CartItemDto> result = _mapper.Map<List<CartItemDto>>(cartItems);
            return result;
        }

        public async Task UpdateCartItem(int id, UpdateCartItem input)
        {
            var cartItem = await CartItemExist(id);
            if (Equals(cartItem, null)) throw new CartItemNotFoundException(id);

            cartItem.Id = input.Id;
            cartItem.Quantity = input.Quantity;

            CartItemValidator validator = new();
            validator.ValidateAndThrowException(cartItem);

            _unitOfWork.CartItems.Update(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<CartItem?> CartItemExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var Order = await _unitOfWork.CartItems.GetById(id);
            return Order;
        }
    }
}