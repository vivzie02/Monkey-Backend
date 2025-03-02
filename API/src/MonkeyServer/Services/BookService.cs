using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkeyServer;
using MonkeyServer.Database;
using MonkeyServer.DTOs;
using MonkeyServer.Mapper;
using System;
using System.Threading.Tasks;

namespace MonkeyServer.Services
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

            log.Info($"saving book");

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
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
