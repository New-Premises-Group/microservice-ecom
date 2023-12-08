using IW.Interfaces;
using IW.Models.DTOs.Review;
using Mapster;
using IW.Models.Entities;
using HotChocolate.Authorization;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class ReviewMutation
    {
        [AllowAnonymous]
        public async Task<string> CreateReview(
            CreateReview input, 
            [Service] IUnitOfWork unitOfWork)
        {
            Review newReview = input.Adapt<Review>();
            newReview.CreatedDate = DateTime.Now.ToUniversalTime();
            newReview.UpdatedDate = DateTime.Now.ToUniversalTime();

            unitOfWork.Reviews.Add(newReview);
            await unitOfWork.CompleteAsync();
            return "Review succesfully created";
        }

        [AllowAnonymous]
        public async Task<string> UpdateReview(
            UpdateReview input,
            [Service] IUnitOfWork unitOfWork)
        {
            var review=await unitOfWork.Reviews.GetById(input.Id);
            if(review == null)
            {
                return "Review not found";
            }

            review.Detail=input.Detail ?? review.Detail;
            review.Rating=input.Rating ?? review.Rating;
            review.UpdatedDate= DateTime.Now.ToUniversalTime();

            unitOfWork.Reviews.Update(review);
            await unitOfWork.CompleteAsync();

            return "Review successfully updated";
        }

        [AllowAnonymous]
        public async Task<string> DeleteReview(
            DeleteReview input,
            [Service] IUnitOfWork unitOfWork)
        {
            var review= await unitOfWork.Reviews.GetById(input.Id);

            if (review == null)
            {
                return "Review not found";
            }

            unitOfWork.Reviews.Remove(review);
            await unitOfWork.CompleteAsync();
            return "Review successfully deleted";
        }
    }
}
