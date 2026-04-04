using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interface
{
    public interface IBorrowingRepository
    {
        Task<List<BorrowingModel>> GetAllBorrowingsAsync();
        // أضيفي علامة الاستفهام هنا ليتطابق مع الـ Implementation والـ Query
        Task<BorrowingModel?> GetBorrowingByIdAsync(int id);
        Task AddBorrowingAsync(BorrowingModel borrowing);
        Task UpdateBorrowingAsync(BorrowingModel borrowing);
        Task<bool> DeleteBorrowingAsync(int id);
        Task<List<BorrowingModel>> GetBorrowingsByUserIdAsync(String userId);
        Task<List<BorrowingModel>> GetActiveBorrowingsAsync();
    }
}