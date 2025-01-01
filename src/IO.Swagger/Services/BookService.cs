using IO.Swagger.Database;
using IO.Swagger.DTOs;
using IO.Swagger.Mapper;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IO.Swagger.Services
{
    /// <summary>
    /// Service for saving and retrieving books
    /// </summary>
    public class BookService: IBookService
    {
        private readonly BookContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BookService(BookContext context) { 
            _context = context;
        }
        /// <summary>
        /// Save a new book
        /// </summary>
        /// <returns></returns>
        public async Task<BookDTO> SaveBook(BookDTO book)
        {
            var bookEntity = BookMapper.ToEntity(book);

            try
            {
                _context.Books.Add(bookEntity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                Console.WriteLine("Successfully created new Book");
                return BookMapper.ToDTO(bookEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return null;
            }
        }
    }
}
