using IO.Swagger.DTOs;
using IO.Swagger.Models;
using System.Threading.Tasks;

namespace IO.Swagger.Services
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
        Task<BookOutputDTO> SaveBook(BookDTO book);
    }
}
