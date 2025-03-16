using System.Runtime.Serialization;
using System;

namespace MonkeyServer.DTOs
{
    /// <summary>
    /// CreateUserOutputDTO
    /// </summary>
    public class CreateUserOutputDTO
    {
        /// <summary>
        /// Gets or Sets UserId
        /// </summary>

        [DataMember(Name = "userId")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}
