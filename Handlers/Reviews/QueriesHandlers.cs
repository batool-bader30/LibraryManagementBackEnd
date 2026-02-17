using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static LibraryManagement.CQRS.Query.ReviewQueries;

namespace LibraryManagement.query
{
    public class ReviewQueryHandlers
    {
        private readonly IReviewRepository _repo;

        public ReviewQueryHandlers(IReviewRepository repo)
        {
            _repo = repo;
        }

        public class GetAllReviewsHandler : IRequestHandler<GetAllReviewsQuery, List<ReviewModel>>
        {
            private readonly IReviewRepository _repo;

            public GetAllReviewsHandler(IReviewRepository repo)
            {
                _repo = repo;
            }

            public async Task<List<ReviewModel>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetAllReviewsAsync();
            }
        }

        public class GetReviewByIdHandler : IRequestHandler<GetReviewByIdQuery, ReviewModel>
        {
            private readonly IReviewRepository _repo;

            public GetReviewByIdHandler(IReviewRepository repo)
            {
                _repo = repo;
            }

            public async Task<ReviewModel> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
            {
                var review = await _repo.GetReviewByIdAsync(request.Id);
                if (review == null)
                    throw new ValidationException("Review not found.");

                return review;
            }
        }

        public class GetReviewsByBookIdHandler : IRequestHandler<GetReviewsByBookIdQuery, List<ReviewModel>>
        {
            private readonly IReviewRepository _repo;

            public GetReviewsByBookIdHandler(IReviewRepository repo)
            {
                _repo = repo;
            }

            public async Task<List<ReviewModel>> Handle(GetReviewsByBookIdQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetReviewsByBookIdAsync(request.BookId);
            }
        }

        public class GetReviewsByUserIdHandler : IRequestHandler<GetReviewsByUserIdQuery, List<ReviewModel>>
        {
            private readonly IReviewRepository _repo;

            public GetReviewsByUserIdHandler(IReviewRepository repo)
            {
                _repo = repo;
            }

            public async Task<List<ReviewModel>> Handle(GetReviewsByUserIdQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetReviewsByUserIdAsync(request.UserId);
            }
        }
    }
}
