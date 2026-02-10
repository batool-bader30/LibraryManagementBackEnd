using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
   
    
  public class AuthorDto
{
    
    public required string Name { get; set; }
    public required string Bio { get; set; }
}

    
}