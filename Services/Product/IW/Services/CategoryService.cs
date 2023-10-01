using IW.Common;
using IW.Exceptions.ReadCategoryError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.CategoryDto;
using IW.Models.Entities;

namespace IW.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCategory(CreateCategory input)
        {
            Category newCategory = new()
            {
                Description = input.Description,
                Name = input.Name,
            };

            CategoryValidator validator = new();
            validator.ValidateAndThrowException(newCategory);

            _unitOfWork.Categories.Add(newCategory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await CategoryExist(id);
            if (Equals(category, null)) throw new CategoryNotFoundException(id);

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAll((int)PAGINATING.OffsetDefault, (int)PAGINATING.AmountDefault);
            ICollection<CategoryDto> result = new List<CategoryDto>();
            foreach (var category in categories)
            {
                CategoryDto item = new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                };
                result.Add(item);
            }
            return result;
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            var category = await CategoryExist(id);
            if (Equals(category, null))
            {
                throw new CategoryNotFoundException(id);
            }
            CategoryDto result = new()
            {
                Id = id,
                Name = category.Name,
                Description = category.Description,
            };
            return result;
        }

        public async Task UpdateCategory(int id, UpdateCategory input)
        {
            var category = await CategoryExist(id);
            if (Equals(category, null)) throw new CategoryNotFoundException(id);

            category.Name = input.Name ?? category.Name;
            category.Description = input.Description ?? category.Description;

            CategoryValidator validator = new ();
            validator.ValidateAndThrowException(category);

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Category?> CategoryExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var category = await _unitOfWork.Categories.GetById(id);
            return category;
        }
    }
}
