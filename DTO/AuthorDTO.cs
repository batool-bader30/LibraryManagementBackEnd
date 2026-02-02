using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
   
    
        public class AuthorDto
{
    [Required]
    public string Name { get; set; }=string.Empty;
    public string? Bio { get; set; }
}

    
}