using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IO.Swagger.Models
{
    /// <summary>
    /// BookEntity
    /// </summary>
    [Table("books")]
    public class Book
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        [Column("content")]
        [Required(ErrorMessage = "Content cannot be empty")]
        public string Content { get; set; }
        /// <summary>
        /// Number of Words
        /// </summary>
        [Column("number_of_words")]
        [Required(ErrorMessage = "Number of words cannot be empty")]
        public int NumberOfWords { get; set; }
    }
}
