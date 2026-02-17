using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.CQRS.command
{
    public class BookCommands
    {
       
      
        public record CreateBookCommand(CreateBookDTO Book) : IRequest<int>;
        public record DeleteBookByIdCommand(int Id) : IRequest<bool>;
        public record UpdateBookCommand(int Id, BookDTO Book) : IRequest<bool>;
    }

    }
