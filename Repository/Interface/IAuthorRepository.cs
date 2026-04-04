using LibraryManagement.Models;

namespace LibraryManagement.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorModel>> GetAuthorsAsync();
        Task<List<AuthorModel>> GetAuthorByNameAsync(string name);
        Task<AuthorModel?> GetAuthorByIdAsync(int id); // إضافة هاد
        Task CreateAuthor(AuthorModel authormodel);
        Task UpdateAuthorAsync(AuthorModel author); // إضافة هاد
        Task<bool> DeletAuthorbyid(int id);
    }
}