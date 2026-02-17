using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.DTO
{

    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? ImageFile { get; set; }

        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<int> CategoryIds { get; set; } = new();

        public ICollection<BorrowingModel> Borrowings { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; }
    }

    public class CreateBookDTO
    {
        public string Title { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<int> CategoryIds { get; set; } = new();

    }

    public class UpdateBookDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }

}