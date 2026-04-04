using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.command
{
     public static class ReviewCommands
    {
        public record CreateReviewCommand(CreateReviewDTO Review) : IRequest<int>;
        public record DeleteReviewByIdCommand(int Id) : IRequest<bool>;
        public record UpdateReviewCommand(int Id, UpdateReviewDTO Review) : IRequest<bool>;
    }
}