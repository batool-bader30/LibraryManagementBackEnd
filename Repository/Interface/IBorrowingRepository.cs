using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interface
{
    public interface IBorrowingRepository
    {
        Task<List<BorrowingModel>> GetAllBorrowingsAsync();
        Task<BorrowingModel> GetBorrowingByIdAsync(int id);
        Task AddBorrowingAsync(BorrowingModel borrowing);
        Task UpdateBorrowingAsync(BorrowingModel borrowing);
        Task<bool> DeleteBorrowingAsync(int id);
        Task<List<BorrowingModel>> GetBorrowingsByUserIdAsync(int userId);
        Task<List<BorrowingModel>> GetActiveBorrowingsAsync();
    }
}
