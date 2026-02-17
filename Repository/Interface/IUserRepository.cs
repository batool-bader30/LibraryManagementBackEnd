using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interface
{
    public interface IUserRepository
    {
        Task<string> CreateUserAsync(UserModel user, string password);
        Task<string> UpdateUserAsync(UserModel user);
        Task<bool> DeleteUserAsync(string id);
        Task<UserModel?> GetUserByIdAsync(string id);
        Task<UserModel?> GetUserByUserNameAsync(string userName);
        Task<List<UserModel>> GetAllUsersAsync();
    }
}
