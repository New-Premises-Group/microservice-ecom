using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.CategoryDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class CategoryQuery
    {
        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<IEnumerable<CategoryDto>> GetCategories([Service] ICategoryService categoryService)
        {
            var results = await categoryService.GetCategories();
            return results;
        }

        [Authorize]
        public async Task<CategoryDto> GetCategory(int id, [Service] ICategoryService categoryService)
        {
            var result = await categoryService.GetCategory(id);
            return result;
        }
    }
}
