using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LibraryManagement.DTO
{
    // 1. DTO للعرض في القوائم (مثلاً الصفحة الرئيسية أو البحث)
    // خفيف جداً عشان السرعة في الـ Flutter
    public class BookSimpleDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? AuthorName { get; set; }
        public bool IsAvailable { get; set; }
    }

    // 2. DTO للعرض التفصيلي (صفحة الكتاب الكاملة)
    // يحتوي على كل البيانات المتداخلة بدون ما يسبب Recycle
    public class BookDetailedDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string ISBN { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
         public string? PageNumber { get; set; }
        public string? Language { get; set; }
        public string? PublishDate { get; set; }



        // بيانات المؤلف الأساسية
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }

        // قائمة أسماء التصنيفات (Strings) لسهولة العرض
        public List<string> Categories { get; set; } = new();

        // مراجعات الكتاب (تستخدم ReviewDTO بسيط لقطع الـ Loop)
        public List<ReviewDTO> Reviews { get; set; } = new();

        // سجل الاستعارة (اختياري)
        public List<BorrowingResponseDTO> Borrowings { get; set; } = new();
    }

    // 3. DTO لعملية الإضافة (Create)
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
         public string? PageNumber { get; set; }
        public string? Language { get; set; }
        public string? PublishDate { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }

    // 4. DTO لعملية التعديل (Update)
    public class UpdateBookDTO
    {
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }
}