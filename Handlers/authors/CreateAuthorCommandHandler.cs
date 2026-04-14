using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.command.AuthorCommands;

namespace LibraryManagement.Handlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorResponseDTO>
    {
        private readonly IAuthorRepository _repo;

        public CreateAuthorCommandHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<AuthorResponseDTO> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            // 1. التأكد أن المؤلف غير موجود مسبقاً
            var result = await _repo.GetAuthorByNameAsync(request.Author.Name);

            if (result != null && result.Count > 0)
                throw new Exception("Author already exists!");

            // 2. تحويل الـ DTO إلى Model (إضافة ImageUrl هنا)
            var author = new AuthorModel
            {
                Name = request.Author.Name,
                Bio = request.Author.Bio,
                ImageUrl = request.Author.ImageUrl // <--- التعديل: ربط الصورة بالموديل
            };

            // 3. الحفظ في قاعدة البيانات
            await _repo.CreateAuthor(author);

            // 4. إرجاع الـ AuthorResponseDTO مع الـ ImageUrl
            return new AuthorResponseDTO
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                ImageUrl = author.ImageUrl, // <--- التعديل: إرجاع الرابط للـ Frontend
                Books = new List<BookSimpleDTO>() 
            };
        }
    }
}