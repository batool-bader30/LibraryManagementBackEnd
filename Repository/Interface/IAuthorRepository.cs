using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LibraryManagement.DTO;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories.Interfaces
{
  public interface IAuthorRepository
  {
    Task<List<AuthorModel>> GetAuthorsAsync();
    Task<List<AuthorModel>> GetAuthorByNameAsync(string name);
    Task CreateAuthor(AuthorModel authormodel);
    Task<bool> DeletAuthorbyid(int id);

  }

}
