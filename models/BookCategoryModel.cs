using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
   public class BookCategoryModel
{
    public int BookId { get; set; }
    public BookModel Book { get; set; }

    public int CategoryId { get; set; }
    public CategoryModel Category { get; set; }}}


