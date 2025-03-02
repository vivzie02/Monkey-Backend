using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.DTOs
{
    /// <summary>
    /// MessageDTO
    /// </summary>
    [DataContract]
    public partial class MessageDTO
    {
        /// <summary>
        /// Gets or Sets Message
        /// </summary>

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
