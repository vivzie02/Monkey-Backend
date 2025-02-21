using IO.Swagger.Database;
using IO.Swagger.DTOs;
using IO.Swagger.Mapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IO.Swagger.Services
{
    /// <summary>
    /// Service for saving and retrieving books
    /// </summary>
    public class BookService : IBookService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public BookService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        /// <summary>
        /// Save a new book
        /// </summary>
        /// <returns></returns>
        public async Task<BookOutputDTO> SaveBook(BookDTO book)
        {
            var bookEntity = BookMapper.ToEntity(book);

            Console.WriteLine($"saving book {book.ToJson()}");

            try
            {
                using(var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();
                    dbContext.Books.Add(bookEntity);
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
                    log.Info("Successfully saved new Book");
                    return BookMapper.ToOutputDTO(bookEntity);
                }
            }
            catch (Exception ex)
            {
                log.Error("could not upload book", ex);
                return null;
            }
        }
    }
}
