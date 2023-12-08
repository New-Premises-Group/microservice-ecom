using GreenDonut;
using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Review;
using IW.Models.Entities;
using Mapster;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class ReviewQuery
    {
        public async Task<ReviewDto> GetReview(
            int id, 
            [Service] IUnitOfWork unitOfWork)
        {
            var result = await unitOfWork.Reviews.GetById(id);
            return result.Adapt<ReviewDto>();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviews(
            [Service] IUnitOfWork unitOfWork, 
            int page = ((int)PAGINATING.OffsetDefault),
            int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await unitOfWork.Reviews.GetAll(page, amount);
            return results.Adapt<IEnumerable<ReviewDto>>();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserId(
            GetReview query, 
            [Service] IUnitOfWork unitOfWork, 
            int page = ((int)PAGINATING.OffsetDefault), 
            int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await unitOfWork
                .Reviews
                .FindByConditionToList(review => 
                review.UserId == query.UserId &&
                 review.ProductId == query.ProductId,
                page, amount);
            return results.Adapt<IEnumerable<ReviewDto>>();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductId(
            GetReview query,
            [Service] IUnitOfWork unitOfWork,
            int page = ((int)PAGINATING.OffsetDefault),
            int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await unitOfWork
                .Reviews
                .FindByConditionToList(review =>
                review.ProductId == query.ProductId,
                page, amount);
            return results.Adapt<IEnumerable<ReviewDto>>();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByRating(
            GetReview query,
            [Service] IUnitOfWork unitOfWork,
            int page = ((int)PAGINATING.OffsetDefault),
            int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await unitOfWork
                .Reviews
                .FindByConditionToList(review =>
                 review.ProductId == query.ProductId &&
                review.UserId == query.UserId,
                page, amount);
            return results.Adapt<IEnumerable<ReviewDto>>();
        }
    }
}
