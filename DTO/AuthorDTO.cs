using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
   public class AuthorResponseDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Bio { get; set; }
    
    // التعديل هنا: نستخدم النسخة الخفيفة من الكتاب
    public List<BookSimpleDTO> Books { get; set; } = new List<BookSimpleDTO>();
}

    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public string? Bio { get; set; }

    }

    public class UpdateAuthorDTO
    {
        public string Name { get; set; }
        public string? Bio { get; set; }

    }



}