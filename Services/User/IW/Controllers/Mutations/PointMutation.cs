using IW.Exceptions.CreateUserError;
using IW.Interfaces.Services;
using IW.Interfaces;
using IW.Models.DTOs.User;
using HotChocolate.Authorization;
using IW.Common;
using IW.Models.Entities;
using Mapster;
using IW.Exceptions.CreateRoleError;
using IW.Models.DTOs.Role;
using IW.Models.DTOs.Point;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class PointMutation
    {
        [AllowAnonymous]
        public async Task<string> CreatePoint(
            CreatePoint input , 
            [Service] IUnitOfWork unitOfWork)
        {
            LoyaltyPoints newPoint= input.Adapt<LoyaltyPoints>();
            unitOfWork.Points.Add(newPoint);
            await unitOfWork.CompleteAsync();
            return "Point successfully created";
        }

        [AllowAnonymous]
        public async Task<string> UpdatePoint(
            UpdatePoint input, 
            [Service] IUnitOfWork unitOfWork)
        {
            LoyaltyPoints point = await unitOfWork.Points.GetById(input.Id);
            if(point == null)
            {
                return "Point not found";
            }
            point.Point = input.Point;

            unitOfWork.Points.Update(point);
            await unitOfWork.CompleteAsync();
            return "Point successfully updated";
        }

        [AllowAnonymous]
        public async Task<string> AddPoint(
            UpdatePoint input,
            [Service] IUnitOfWork unitOfWork)
        {
            LoyaltyPoints point = await unitOfWork.Points.GetById(input.Id);
            if (point == null)
            {
                return "Point not found";
            }
            point.Point += input.Point;

            unitOfWork.Points.Update(point);
            await unitOfWork.CompleteAsync();
            return "Point successfully updated";
        }

        [AllowAnonymous]
        public async Task<string> SubtractPoint(
            UpdatePoint input,
            [Service] IUnitOfWork unitOfWork)
        {
            LoyaltyPoints point = await unitOfWork.Points.GetById(input.Id);
            if (point == null)
            {
                return "Point not found";
            }
            point.Point -= input.Point;

            unitOfWork.Points.Update(point);
            await unitOfWork.CompleteAsync();
            return "Point successfully updated";
        }
    }
}
