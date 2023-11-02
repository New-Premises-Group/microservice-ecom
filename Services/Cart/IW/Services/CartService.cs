using IW.Exceptions.ReadCartError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs;
using IW.Models.DTOs.Cart;
using IW.Models.Entities;
using MapsterMapper;

namespace IW.Services
{
    public class CartService : ICartService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateCart(CreateCart input)
        {
            Cart newCart = _mapper.Map<Cart>(input);

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
            CartDto result = _mapper.Map<CartDto>(cart);
            return result;
        }

        public async Task<IEnumerable<CartDto>> GetCarts(int offset, int amount)
        {
            var carts = await _unitOfWork.Carts.GetAll(offset, amount);
            ICollection<CartDto> result = _mapper.Map<List<CartDto>>(carts);
            return result;
        }

        public async Task<IEnumerable<CartDto>> GetCarts(GetCart query, int offset, int amount)
        {
            var carts = await _unitOfWork.Carts.FindByConditionToList(
                p => p.UserId == query.UserId
                , offset, amount);
            ICollection<CartDto> result = _mapper.Map<List<CartDto>>(carts);
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