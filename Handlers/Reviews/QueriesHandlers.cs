using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using LibraryManagement.CQRS.Query;
using LibraryManagement.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static LibraryManagement.CQRS.ReviewQueries;

namespace LibraryManagement.query
{
    public class ReviewQueryHandlers : 
        IRequestHandler<GetAllReviewsQuery, List<ReviewDTO>>,
        IRequestHandler<GetReviewByIdQuery, ReviewDTO?>,
        IRequestHandler<GetReviewsByBookIdQuery, List<ReviewDTO>>,
        IRequestHandler<GetReviewsByUserIdQuery, List<ReviewDTO>>
    {
        private readonly IReviewRepository _repo;

        public ReviewQueryHandlers(IReviewRepository repo) => _repo = repo;

        public async Task<List<ReviewDTO>> Handle(GetAllReviewsQuery request, CancellationToken ct)
        {
            var reviews = await _repo.GetAllReviewsAsync();
            return reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating
            }).ToList();
        }

        public async Task<ReviewDTO?> Handle(GetReviewByIdQuery request, CancellationToken ct)
        {
            var r = await _repo.GetReviewByIdAsync(request.Id);
            if (r == null) return null;
            return new ReviewDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating
            };
        }

        public async Task<List<ReviewDTO>> Handle(GetReviewsByBookIdQuery request, CancellationToken ct)
        {
            var reviews = await _repo.GetReviewsByBookIdAsync(request.BookId);
            return reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating
            }).ToList();
        }

        public async Task<List<ReviewDTO>> Handle(GetReviewsByUserIdQuery request, CancellationToken ct)
        {
            var reviews = await _repo.GetReviewsByUserIdAsync(request.UserId);
            return reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating
            }).ToList();
        }
    }
}