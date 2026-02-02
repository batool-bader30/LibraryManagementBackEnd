using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.data;
using LibraryManagement.DTO;
using LibraryManagement.models;
using LibraryManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository.Implementation
{
  public class AuthorRepo : IAuthorRepository 
{
    private readonly addDBcontext _context;

    public AuthorRepo(addDBcontext context)
    {
        _context = context;
    }

    public async Task<List<Authormodel>> GetAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<List<Authormodel>> GetAuthorByNameAsync(string name)
    {
        return await _context.Authors.Where(a => a.Name.Contains(name)).ToListAsync();
    
    }
    public async Task CreateAuthor(Authormodel authormodel)
    {
        await _context.Authors.AddAsync(authormodel);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeletAuthorbyid(int id)
    {
        var author = await _context.Authors.FindAsync(id);
    if (author == null) return false;

    _context.Authors.Remove(author);
    await _context.SaveChangesAsync();
    return true;
    }
    

}

    
}
      

        

      
         