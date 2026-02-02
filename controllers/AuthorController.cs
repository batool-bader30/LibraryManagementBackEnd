using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.data;
using LibraryManagement.DTO;
using LibraryManagement.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthorController : ControllerBase
    {
        private readonly addDBcontext _context;

        // نستخدم الـ Constructor لجلب الـ DbContext الذي ربطناه سابقاً
        public AuthorController(addDBcontext context)
        {
            _context = context;
        }

        [HttpPost("postauthor")]
     public async Task<IActionResult> PostAuthor([FromBody] AuthorDto authorDto)
{
    if (await _context.Authors.AnyAsync(a => a.Name == authorDto.Name))
    {
        return BadRequest("Author already exists!");
    }

    var author = new Authormodel
    {
        Name = authorDto.Name,
        Bio = authorDto.Bio
    };

    await _context.Authors.AddAsync(author);
    await _context.SaveChangesAsync();
    return Ok(author);
}

        [HttpGet("getauthor")]
        public async Task<IActionResult> GetAuthors()
        {
             if (!await _context.Authors.AnyAsync())
            {
                return BadRequest("No Authors exists!");
            }
            else{
          var Authors= await _context.Authors.ToListAsync();
          
          return Ok(Authors);
            }
        }

        [HttpGet("getauthorbyname")]
        public async Task<IActionResult> GetAuthorbyname(string name)
        {
                var authors = await _context.Authors.Where(a => a.Name == name)
                                .ToListAsync();;

              if (authors.Count==0)
            {
                return BadRequest(name+ " not found!");
            }
            else{
          
          return Ok(authors);
            }
        }
       [HttpDelete("deleteauthor/{id}")]
        public async Task<IActionResult> DeletAuthorbyid(int id)
        {
            var Author=await _context.Authors.FindAsync(id);
             if (Author==null)
            {
                return BadRequest("Author not found!");
            }
            else{
           _context.Authors.Remove(Author);
           await _context.SaveChangesAsync();
          return Ok(Author.Name+" was deleted");
            }
        }
    }
}