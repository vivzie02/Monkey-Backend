using Microsoft.EntityFrameworkCore;
using MonkeyServer.Entities;

namespace MonkeyServer.Database
{
    /// <summary>
    /// UserContext
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Users Table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        /// <summary>
        /// ModelCreation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}
