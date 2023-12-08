using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class ReviewRepository : GenericRepository<Review>,IReviewRepository
    {
        public ReviewRepository(AppDbContext context):base(context)
        {
        }
    }
}
