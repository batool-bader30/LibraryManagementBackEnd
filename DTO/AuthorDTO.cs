using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
public class AuthorDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
        public string? Bio { get; set; }

}

public class CreateAuthorDTO
{
    public string Name { get; set; }
}

public class UpdateAuthorDTO
{
    public string Name { get; set; }
}



}