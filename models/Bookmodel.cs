using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Author (One-to-Many)
        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }

        // Many-to-Many with Category
        public ICollection<BookCategoryModel> BookCategories { get; set; }

        public ICollection<BorrowingModel> Borrowings { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; }
    }

}
