using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
   
    public class GetBooksDto
{
    public required string Title { get; set; }
    public  required string Description { get; set; }
    public required double Price { get; set; }
    public required int PublishYear { get; set; }
    public required string ISBN { get; set; }
    public required int AuthorId { get; set; }

    public IFormFile? ImageUrl { get; set; } 

    }
}