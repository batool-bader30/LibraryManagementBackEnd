using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public int UserId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}

public class UpdateReviewDTO
{
    public string Comment { get; set; }
    public int Rating { get; set; }
}

}