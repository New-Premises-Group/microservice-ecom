using IW.Models.DTOs.CategoryDto;

namespace IW.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategory(int id);
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task UpdateCategory(int id, UpdateCategory input);
        Task DeleteCategory(int id);
        Task CreateCategory(CreateCategory input);
    }
}
