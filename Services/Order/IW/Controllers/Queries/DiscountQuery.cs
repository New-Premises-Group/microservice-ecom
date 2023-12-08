using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.DiscountDtos;
using IW.Models.Entities;
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

        [AllowAnonymous]
        [UseProjection]
        public IQueryable<Discount> GetAvailableDiscounts(
            [Service] IDiscountRepository discountRepository,
            DiscountConditionDto condition,
            int page = (int)PAGINATING.OffsetDefault,
            int amount = (int)PAGINATING.AmountDefault)
        {
            var results = discountRepository
                .FindByConditionToQuery(
                discount => 
                discount.ActiveDate <= DateTime.Now.ToUniversalTime() &&
                discount.ExpireDate >= DateTime.Now.ToUniversalTime() &&
                (
                discount.TotalOverCondition <= condition.Total ||
                discount.SpecialDayCondition.Equals(condition.SpecialDay.Value) ||
                discount.BirthdayCondition.Month.Equals(condition.Birthday.Value.Month)
                ),
                page, amount);

            return results;
        }
    }
}
