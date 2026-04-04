namespace LibraryManagement.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }

    public class CreateReviewDTO
    {
        public int BookId { get; set; }
        public string UserId { get; set; } 
        public string Comment { get; set; }
        public int Rating { get; set; } // يجب أن يكون بين 1 و 5
    }

    public class UpdateReviewDTO
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}