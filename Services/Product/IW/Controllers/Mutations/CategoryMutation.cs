using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateCategoryError;
using IW.Interfaces.Services;
using IW.Models.DTOs.CategoryDto;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class CategoryMutation
    {
        [Error(typeof(CreateCategoryErrorFactory))]
        public async Task<CategoryCreatedPayload> CreateCategory(CreateCategory input, [Service] ICategoryService categoryService)
        {
            await categoryService.CreateCategory(input);
            var payload = new CategoryCreatedPayload()
            {
                Message = "Category successfully created"
            };
            return payload;
        }
        [Error(typeof(CreateCategoryErrorFactory))]
        public async Task<CategoryCreatedPayload> UpdateCategory(int id, UpdateCategory input, [Service] ICategoryService categoryService)
        {
            await categoryService.UpdateCategory(id, input);
            var payload = new CategoryCreatedPayload()
            {
                Message = "Category successfully updated"
            };
            return payload;
        }

        [Error(typeof(CreateCategoryErrorFactory))]
        public async Task<CategoryCreatedPayload> DeleteCategory(int id, [Service] ICategoryService categoryService)
        {
            await categoryService.DeleteCategory(id);
            var payload = new CategoryCreatedPayload()
            {
                Message = "Category successfully deleted"
            };
            return payload;
        }
    }
}
