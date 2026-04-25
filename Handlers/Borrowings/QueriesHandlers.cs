using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using LibraryManagement.DTO;
using static LibraryManagement.CQRS.Query.ReviewQueries;

namespace LibraryManagement.query
{
    public class BorrowingQueryHandlers : 
        IRequestHandler<GetAllBorrowingsQuery, List<BorrowingResponseDTO>>,
        IRequestHandler<GetBorrowingByIdQuery, BorrowingResponseDTO?>,
        IRequestHandler<GetBorrowingsByUserIdQuery, List<BorrowingResponseDTO>>
    {
        private readonly IBorrowingRepository _repo;
        public BorrowingQueryHandlers(IBorrowingRepository repo) => _repo = repo;

        public async Task<List<BorrowingResponseDTO>> Handle(GetAllBorrowingsQuery request, CancellationToken ct)
        {
            var data = await _repo.GetAllBorrowingsAsync();
            return data.Select(Map).ToList();
        }

        public async Task<BorrowingResponseDTO?> Handle(GetBorrowingByIdQuery request, CancellationToken ct)
        {
            var b = await _repo.GetBorrowingByIdAsync(request.Id);
            return b == null ? null : Map(b);
        }

        public async Task<List<BorrowingResponseDTO>> Handle(GetBorrowingsByUserIdQuery request, CancellationToken ct)
        {
            var data = await _repo.GetBorrowingsByUserIdAsync(request.UserId);
            return data.Select(Map).ToList();
        }

       private BorrowingResponseDTO Map(BorrowingModel b)
{
    var response = new BorrowingResponseDTO
    {
        Id = b.Id,
        BorrowDate = b.BorrowDate,
        ReturnDate = b.ReturnDate,
        Status = b.Status.ToString()
    };

    if (b.Book != null)
    {
        response.Book = new BookSimpleDTO
        {
            Id = b.Book.Id,
            Title = b.Book.Title,
            AuthorName=b.Book.Author.Name,
            ImageUrl = b.Book.ImageUrl,
            IsAvailable = b.Book.IsAvailable
        };
    }

    if (b.User != null)
    {
        response.User = new UserSimpleDTO
        {
            Id = b.User.Id,
            UserName = b.User.UserName,
            Email = b.User.Email,
            PhoneNumber = b.User.PhoneNumber
        };
    }

    return response;
}
    }
}