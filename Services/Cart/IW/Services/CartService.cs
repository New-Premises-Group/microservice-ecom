using IW.Exceptions.ReadCartError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs;
using IW.Models.DTOs.Cart;
using IW.Models.Entities;

namespace IW.Services
{
    public class CartService : ICartService
    {
        public readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCart(CreateCart input)
        {
            Cart newCart = new()
            {
                UserId = input.UserId
            };

            CartValidator validator = new();
            validator.ValidateAndThrowException(newCart);

            _unitOfWork.Carts.Add(newCart);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<CartDto> GetCart(int id)
        {
            var cart = await CartExist(id);
            if (Equals(cart, null))
            {
                throw new CartNotFoundException(id);
            }
            var order = await _unitOfWork.CartItems.GetById(cart.Id);
            CartDto result = new()
            {
                UserId = cart.UserId
            };
            return result;
        }

        public async Task<IEnumerable<CartDto>> GetCarts(int offset, int amount)
        {
            var carts = await _unitOfWork.Carts.GetAll(offset, amount);
            ICollection<CartDto> result = new List<CartDto>();
            foreach (var cart in carts)
            {
                CartDto newCart = new()
                {
                    UserId = cart.UserId,
                };
                result.Add(newCart);
            }
            return result;
        }

        public async Task<IEnumerable<CartDto>> GetCarts(GetCart query, int offset, int amount)
        {
            var carts = await _unitOfWork.Carts.FindByConditionToList(
                p => p.UserId == query.UserId
                , offset, amount);
            ICollection<CartDto> result = new List<CartDto>();
            foreach (var cart in carts)
            {
                CartDto newCart = new()
                {
                    UserId = cart.UserId,
                };
                result.Add(newCart);
            }
            return result;
        }

        public async Task UpdateCart(int id, UpdateCart model)
        {
            var cart = await CartExist(id);
            if (Equals(cart, null)) throw new CartNotFoundException(id);

            cart.CartItems = model.CartItems ?? cart.CartItems;

            CartValidator validator = new();
            validator.ValidateAndThrowException(cart);

            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCart(int id)
        {
            var cart = await CartExist(id);
            if (Equals(cart, null)) throw new CartNotFoundException(id);

            _unitOfWork.Carts.Remove(cart);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Cart?> CartExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = await _unitOfWork.Carts.GetById(id);
            return result;
        }
    }
}