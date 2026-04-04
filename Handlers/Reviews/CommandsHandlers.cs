using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static LibraryManagement.command.ReviewCommands;

namespace LibraryManagement.command
{
    public class ReviewCommandHandlers
    {
        public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, int>
        {
            private readonly IReviewRepository _repo;
            public CreateReviewHandler(IReviewRepository repo) => _repo = repo;

            public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                // Validation
                if (string.IsNullOrWhiteSpace(request.Review.Comment))
                    throw new ValidationException("Comment cannot be empty.");

                if (request.Review.Rating < 1 || request.Review.Rating > 5)
                    throw new ValidationException("Rating must be between 1 and 5.");

                var review = new ReviewModel
                {
                    BookId = request.Review.BookId,
                    UserId = request.Review.UserId,
                    Comment = request.Review.Comment,
                    Rating = request.Review.Rating
                };

                await _repo.AddReviewAsync(review);
                return review.Id;
            }
        }

        public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, bool>
        {
            private readonly IReviewRepository _repo;
            public UpdateReviewHandler(IReviewRepository repo) => _repo = repo;

            public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
            {
                if (request.Review.Rating < 1 || request.Review.Rating > 5)
                    throw new ValidationException("Rating must be between 1 and 5.");

                var review = new ReviewModel
                {
                    Id = request.Id,
                    Comment = request.Review.Comment,
                    Rating = request.Review.Rating
                };
                await _repo.UpdateReviewAsync(review);
                return true;
            }
        }

        public class DeleteReviewHandler : IRequestHandler<DeleteReviewByIdCommand, bool>
        {
            private readonly IReviewRepository _repo;
            public DeleteReviewHandler(IReviewRepository repo) => _repo = repo;

            public async Task<bool> Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
            {
                return await _repo.DeleteReviewAsync(request.Id);
            }
        }
    }
}