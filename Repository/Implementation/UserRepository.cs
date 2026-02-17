using LibraryManagement.data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;

        public UserRepository(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateUserAsync(UserModel user, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser != null)
                return "Username already exists";

            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? user.Id : string.Join(", ", result.Errors);
        }

        public async Task<string> UpdateUserAsync(UserModel user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null) return "User not found";

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;

            var result = await _userManager.UpdateAsync(existingUser);
            return result.Succeeded ? "Updated" : string.Join(", ", result.Errors);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<UserModel?> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
