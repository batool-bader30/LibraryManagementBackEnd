// داخل ملف GetBooksByCategoryHandler.cs
using LibraryManagement.DTO;
using LibraryManagement.Repository.Interface;
using MediatR;
using static LibraryManagement.query.BookQueries;

public class GetBooksByCategoryHandler : IRequestHandler<GetBooksByCategoryQuery, List<BookSimpleDTO>>
{
    private readonly IBookRepository _repo;
    public GetBooksByCategoryHandler(IBookRepository repo) => _repo = repo;

    public async Task<List<BookSimpleDTO>> Handle(GetBooksByCategoryQuery request, CancellationToken ct)
    {
        var books = await _repo.GetBooksByCategoryIdAsync(request.CategoryId);
        return books.Select(b => new BookSimpleDTO {
            Id = b.Id,
            Title = b.Title,
            ImageUrl = b.ImageUrl,
            AuthorName = b.Author?.Name ?? "Unknown",
            IsAvailable = b.IsAvailable
        }).ToList();
    }
}