using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;

namespace IO.Swagger.Database
{
    /// <summary>
    /// BookContext
    /// </summary>
    public class BookContext : DbContext
    {
        /// <summary>
        /// Books Table
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }

        /// <summary>
        /// ModelCreation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("books");
        }
    }
}
