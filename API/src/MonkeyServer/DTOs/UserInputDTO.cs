using System;
using System.Runtime.Serialization;

namespace MonkeyServer.DTOs
{
    /// <summary>
    /// CreateUserInputDTO
    /// </summary>
    [DataContract]
    public class CreateUserInputDTO
    {
        /// <summary>
        /// Username
        /// </summary>
        [DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

    }
}