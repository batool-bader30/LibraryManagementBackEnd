using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LibraryManagement.models;
using LibraryManagement.DTO;

namespace LibraryManagement.Repositories.Interfaces
{
  public interface IAuthorRepository
{
    Task<List<Authormodel>> GetAuthorsAsync();
    Task<List<Authormodel>> GetAuthorByNameAsync(string name);
    Task CreateAuthor(Authormodel authormodel);
    Task<bool> DeletAuthorbyid(int id);
    
}

}
