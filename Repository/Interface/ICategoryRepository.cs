using LibraryManagement.Models;

public interface ICategoryRepository
{
    Task<List<CategoryModel>> GetAllCategoriesAsync();
    Task<List<CategoryModel>> GetCategoryByNameAsync(string name);
    Task<CategoryModel?> GetCategoryByIdAsync(int id); // إضافة هاد
    Task CreateCategory(CategoryModel category);
    Task UpdateCategoryAsync(CategoryModel category); // إضافة هاد
    Task<bool> DeleteCategoryById(int id);
}