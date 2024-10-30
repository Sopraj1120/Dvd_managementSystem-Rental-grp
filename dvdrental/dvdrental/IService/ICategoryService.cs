using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface ICategoryService
    {
        Task<string> AddCategory(CategoryRequestDto categoryDto);
        Task UpdateCategory(int id, CategoryRequestDto categoryDto);
        Task DeleteCategory(int id);
        Task<List<CategoryResponceDto>> GetCategories();
        Task<CategoryResponceDto> GetCategoryById(int id);

    }
}
