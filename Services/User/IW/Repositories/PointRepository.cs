using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    public class PointRepository : GenericRepository<LoyaltyPoints>,IPointRepository
    {
        public PointRepository(AppDbContext context):base(context)
        {
        }

    }
}
