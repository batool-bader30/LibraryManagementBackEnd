using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
   
        public class BookDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int PublishYear { get; set; }
    public string ISBN { get; set; }
    public int AuthorId { get; set; }

    public string? ImageUrl { get; set; } // 👈 رابط الصورة

    }
}