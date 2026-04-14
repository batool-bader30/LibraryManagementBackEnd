using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.command;
using MediatR;
using static LibraryManagement.CQRS.command.BookCommands;

namespace LibraryManagement.CQRS.Handlers.Book.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken ct)
        {
            // 1. التحقق من الـ ISBN
            var allBooks = await _bookRepository.GetAllBooksAsync();
            if (allBooks.Any(b => b.ISBN == request.Book.ISBN))
                throw new Exception("Book with the same ISBN already exists.");

            // --- بداية منطق رفع الصورة ---
            string dbImagePath = request.Book.ImageUrl ?? ""; // القيمة الافتراضية

            if (request.Book.ImageFile != null && request.Book.ImageFile.Length > 0)
            {
                // أ. تحديد اسم فريد للملف
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Book.ImageFile.FileName);

                // ب. تحديد المسار الكامل للمجلد (wwwroot/images/books)
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "books");

                // ج. التأكد من وجود المجلد
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                // د. حفظ الملف فعلياً على الهاردسك
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Book.ImageFile.CopyToAsync(stream);
                }

                // هـ. تجهيز المسار اللي رح يتخزن في قاعدة البيانات
                dbImagePath = $"/images/books/{fileName}";
            }
            // --- نهاية منطق رفع الصورة ---

            BookModel newBook = new()
            {
                Title = request.Book.Title,
                Description = request.Book.Description,
                ISBN = request.Book.ISBN,
                ImageUrl = dbImagePath, // ✅ نستخدم المسار الجديد هنا
                AuthorId = request.Book.AuthorId,
                IsAvailable = true,
                BookCategories = new List<BookCategoryModel>()
            };

            // ربط الكاتيجوريز
            foreach (var catId in request.Book.CategoryIds)
            {
                newBook.BookCategories.Add(new BookCategoryModel { CategoryId = catId });
            }

            await _bookRepository.AddBookAsync(newBook);
            return newBook.Id;
        }
    }
}
