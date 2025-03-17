using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonkeyServer.Entities
{
    /// <summary>
    /// UserEntity
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// userId
        /// </summary>
        [Key]
        [Column("user_id")]
        public Guid UserId { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        [Column("username")]
        [Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }
        /// <summary>
        /// Salt
        /// </summary>
        [Column("salt")]
        public string Salt { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Column("password")]
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}
