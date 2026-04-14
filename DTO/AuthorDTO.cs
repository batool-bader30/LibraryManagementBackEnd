using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTO
{
    public class AuthorResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        
        // أضفنا رابط الصورة ليتم إرجاعه في الـ Response
        public string? ImageUrl { get; set; } 
        
        public List<BookSimpleDTO> Books { get; set; } = new List<BookSimpleDTO>();
    }

    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Bio { get; set; }

        // أضفنا خاصية الصورة عند إنشاء مؤلف جديد
        public string? ImageUrl { get; set; } 
    }

    public class UpdateAuthorDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Bio { get; set; }

        // أضفنا خاصية الصورة عند التحديث
        public string? ImageUrl { get; set; } 
    }
}