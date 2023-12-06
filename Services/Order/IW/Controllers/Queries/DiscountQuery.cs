using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;
using Mapster;

namespace IW.MessageBroker.Queries
{
    [ExtendObjectType("Query")]
    public class DiscountQuery
    {
        [AllowAnonymous]
        public async Task<IEnumerable<DiscountDto>> GetDiscounts(
            [Service] IDiscountRepository discountRepository, 
            int page = (int)PAGINATING.OffsetDefault, 
            int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await discountRepository.GetAll(page,amount);
            return results.Adapt<IEnumerable<DiscountDto>>();
        }

        //[AllowAnonymous]
        //public async Task<IEnumerable<DiscountDto>> FindOrders(
        //    GetDiscount query,
        //    [Service] IDiscountRepository discountRepository, 
        //    int page = (int)PAGINATING.OffsetDefault, 
        //    int amount = (int)PAGINATING.AmountDefault)
        //{
        //    var results = await orderService.GetOrders(query, page, amount);
        //    return results;
        //}

        [AllowAnonymous]
        public async Task<DiscountDto> GetDiscount(
            int id, 
            [Service] IDiscountRepository discountRepository)
        {
            var result = await discountRepository.GetById(id);
            return result.Adapt<DiscountDto>();
        }
    }
}
