using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.models
{
    public class AuthorModel
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string? Name { set; get; }
        public string? Bio { set; get; }
        public List<BookModel> books { get; set; } = new List<BookModel>();

    }
}