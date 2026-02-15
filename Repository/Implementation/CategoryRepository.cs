using LibraryManagement.data;
using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBcontext _context;

        public CategoryRepository(AppDBcontext context)
        {
            _context = context;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<CategoryModel>> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        public async Task CreateCategory(CategoryModel category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
