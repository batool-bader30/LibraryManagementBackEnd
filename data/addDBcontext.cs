using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.data
{
    public class addDBcontext:DbContext
    {
        public addDBcontext(DbContextOptions <addDBcontext> options):base(options)
        {}
        public DbSet<Authormodel> Authors { get; set; }
        public DbSet<Bookmodel> Books { get; set; }
    
    }
}