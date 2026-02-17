using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.DTO
{
   public class BorrowingDTO
{
    public int BookId { get; set; }
    public string UserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public BorrowStatus Status { get; set; }
}

public class CreateBorrowingDTO
{
    public int BookId { get; set; }
    public int UserId { get; set; }
}

public class UpdateBorrowingDTO
{
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; }
}

}