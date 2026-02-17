using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interface
{
    public interface IReviewRepository
    {
        Task<List<ReviewModel>> GetAllReviewsAsync();
        Task<ReviewModel> GetReviewByIdAsync(int id);
        Task AddReviewAsync(ReviewModel review);
        Task UpdateReviewAsync(ReviewModel review);
        Task<bool> DeleteReviewAsync(int id);
        Task<List<ReviewModel>> GetReviewsByBookIdAsync(int bookId);
        Task<List<ReviewModel>> GetReviewsByUserIdAsync(int userId);
    }
}
