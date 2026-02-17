using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.data;
using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDBcontext _context;

        public AuthorRepository(AppDBcontext context)
        {
            _context = context;
        }

        public async Task<List<AuthorModel>> GetAuthorsAsync()
        {
            return await _context.Authors

     .Include(a => a.books) 
     .ToListAsync();
        }

        public async Task<List<AuthorModel>> GetAuthorByNameAsync(string name)
        {
            return await _context.Authors
     .Where(a => a.Name.Contains(name))
     .Include(a => a.books)
     .ToListAsync();

        }
        public async Task CreateAuthor(AuthorModel authormodel)
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





