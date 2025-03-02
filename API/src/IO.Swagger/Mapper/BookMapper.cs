using IO.Swagger.DTOs;
using IO.Swagger.Models;
using System;

namespace IO.Swagger.Mapper
{
    /// <summary>
    /// BookMapper
    /// </summary>
    public static class BookMapper
    {
        /// <summary>
        /// Book DTO to entity
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        public static Book ToEntity(BookDTO bookDTO)
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Content = bookDTO.Content,
                NumberOfWords = bookDTO.NumberOfWords
            };
        }

        /// <summary>
        /// Book Entity to DTO 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static BookOutputDTO ToOutputDTO(Book book)
        {
            return new BookOutputDTO
            {
                Id = book.Id,
                Content = book.Content,
                NumberOfWords = book.NumberOfWords,
            };
        }
    }
}
