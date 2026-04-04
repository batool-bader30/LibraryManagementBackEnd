using LibraryManagement.Models;

namespace LibraryManagement.DTO
{
    // هذا الـ DTO الذي سيظهر في نتائج البحث (Swagger/Flutter)
    public class BorrowingResponseDTO
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }

        // استخدام الـ DTOs البسيطة التي أنشأتِها سابقاً لقطع الـ Loop
        public BookSimpleDTO Book { get; set; }
        public UserSimpleDTO User { get; set; }
    }

    public class CreateBorrowingDTO
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateBorrowingDTO
    {
        public DateTime? ReturnDate { get; set; }
        public BorrowStatus Status { get; set; }
    }
}