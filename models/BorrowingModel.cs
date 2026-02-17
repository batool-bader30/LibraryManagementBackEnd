using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{

    public enum BorrowStatus
    {
        Borrowing,
        Returned
    }

    public class BorrowingModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public BookModel Book { get; set; }

        public string UserId { get; set; }
        public UserModel User { get; set; }
         public bool IsAvailable { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }

        public BorrowStatus Status { get; set; }=BorrowStatus.Borrowing;
    }

}