using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class ReviewModel
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public BookModel Book { get; set; }

    public string UserId { get; set; }
    public UserModel User { get; set; }

    public string Comment { get; set; }
    public int Rating { get; set; } 
}

}