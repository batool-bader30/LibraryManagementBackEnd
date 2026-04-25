using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;
using LibraryManagement.DTO;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDetailedDTO?>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDetailedDTO> Handle(GetBookByIdQuery request, CancellationToken ct)
        {
            var b = await _bookRepository.GetBookByIdAsync(request.Id);
            if (b == null) throw new Exception("Book not found.");

            return new BookDetailedDTO
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ISBN = b.ISBN,
                ImageUrl = b.ImageUrl,
                IsAvailable = b.IsAvailable,
                AuthorId = b.AuthorId,
                AuthorName = b.Author?.Name,
                PageNumber = b.PageNumber,
                Language = b.Language,
                PublishDate = b.PublishDate,
                Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList(),
                Reviews = b.Reviews.Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                }).ToList(),
                Borrowings = b.Borrowings.Select(r => new BorrowingResponseDTO
                {
                    Id = r.Id,
                    ReturnDate = r.ReturnDate,
                    BorrowDate = r.BorrowDate,
                    Status = r.Status.ToString(),
                    User = new UserSimpleDTO
                    {
                        Id = r.User.Id,
                        UserName = r.User.UserName,
                        Email = r.User.Email,
                        PhoneNumber = r.User.PhoneNumber


                    },
                   
                }).ToList()
            };
        }
    }
}
