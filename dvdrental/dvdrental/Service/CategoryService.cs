using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;

namespace dvdrental.Service
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<string> AddCategory(CategoryRequestDto categoryDto)
        {


            var Category = new Category
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,    

            };
            var data = await _categoryRepository.AddCategory(Category);
            return data;

        }
        public async Task UpdateCategory(int id, CategoryRequestDto categoryDto)
        {

            var existingCategory = await _categoryRepository.GetCategoryById(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }


            existingCategory.Name = categoryDto.Name;


            await _categoryRepository.UpdateCategory(existingCategory);
        }
        public async Task DeleteCategory(int id)
        {

            var existingCategory = await _categoryRepository.GetCategoryById(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }


            await _categoryRepository.DeleteCategory(id);
        }

        public async Task<List<CategoryResponceDto>> GetCategories()
        {

            var categories = await _categoryRepository.GetCategories();

            var returnList= new List<CategoryResponceDto>();
            foreach (var category in categories)
            {
                var obj = new CategoryResponceDto
                {
                    Id = category.Id,
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                };
                returnList.Add(obj);
            }
            return returnList;
           
        }


        public async Task<CategoryResponceDto> GetCategoryById(int id)
        {

            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }


            return  new CategoryResponceDto
            {
                Id=category.Id,
                CategoryId = category.CategoryId,
                Name = category.Name,
            };
        }

    }
}
