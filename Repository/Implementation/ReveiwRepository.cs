using LibraryManagement.data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository.Implementation
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDBcontext _context;

        public ReviewRepository(AppDBcontext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(ReviewModel review)
        {
            var bookExists = await _context.Books
                .AnyAsync(b => b.Id == review.BookId);

            if (!bookExists)
                throw new Exception("Book not found");

            var userExists = await _context.Users
                .AnyAsync(u => u.Id == review.UserId);

            if (!userExists)
                throw new Exception("User not found");

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewModel>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }


        public async Task<ReviewModel?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }



        public async Task<List<ReviewModel>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<List<ReviewModel>> GetReviewsByUserIdAsync(int userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId.ToString())
                .ToListAsync();
        }

        public async Task UpdateReviewAsync(ReviewModel review)
        {
            var existing = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == review.Id);

            if (existing == null)
                throw new Exception("Review not found");

            existing.Comment = review.Comment;
            existing.Rating = review.Rating;

            await _context.SaveChangesAsync();
        }
    }
}
