using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.command
{
     public static class ReviewCommands
    {
        public record CreateReviewCommand(ReviewDTO Review) : IRequest<int>;
        public record DeleteReviewByIdCommand(int Id) : IRequest<bool>;
        public record UpdateReviewCommand(int Id, ReviewModel Review) : IRequest<bool>;
    }
}