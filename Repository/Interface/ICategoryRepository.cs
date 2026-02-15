using LibraryManagement.Models;

namespace LibraryManagement.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<List<CategoryModel>> GetCategoryByNameAsync(string name);
        Task CreateCategory(CategoryModel category);
        Task<bool> DeleteCategoryById(int id);
    }
}
