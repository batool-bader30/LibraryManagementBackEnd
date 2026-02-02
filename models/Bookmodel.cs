using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.models
{
 public class Bookmodel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
    public double Price { get; set; }
    public int PublishYear { get; set; }
    public string ISBN { get; set; } = string.Empty;

    public byte[]? ImageUrl { get; set; }
    public string? ImageType { get; set; }   

    public int AuthorId { get; set; }
    public Authormodel? Author { get; set; }
}

}
