using MonkeyServer.DTOs;
using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// Service for saving and retrieving books
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Save a new book
        /// </summary>
        /// <returns></returns>
        Task<BookOutputDTO> SaveBookAsync(BookDTO book);
    }
}
