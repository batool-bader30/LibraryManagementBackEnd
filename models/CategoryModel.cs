using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

    public ICollection<BookCategoryModel> BookCategories { get; set; }
    }
}
