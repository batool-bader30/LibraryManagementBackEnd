using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public double Price { get; set; }
        public int PublishYear { get; set; }
        public string ISBN { get; set; } = string.Empty;

          public byte[]? ImageUrl { get; set; } // تخزين الصورة مباشرة

        public int AuthorId { get; set; }
        public AuthorModel? Author { get; set; }
    }

}
