using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Point;
using IW.Models.DTOs.User;
using Mapster;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class PointQuery
    {
        public async Task<PointDto> GetPoint(
            int id,
            [Service] IPointRepository pointRepository)
        {
            var point = await pointRepository.GetById(id);
            return point.Adapt<PointDto>();
        }
        public async Task<PointDto?> GetPointByUserId(
        string userId,
        [Service] IPointRepository pointRepository)
        {
            var point = await pointRepository.FindByCondition(
                point => point.UserId.ToString().Equals(userId.ToString()));
            if (point == null)
            {
                return null;
            }
            return point.Adapt<PointDto>();
        }
    }
}
