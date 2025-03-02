using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System;

namespace IO.Swagger.DTOs
{
    /// <summary>
    /// MonkeyOutputDTO
    /// </summary>
    [DataContract]
    public class MonkeyOutputDTO
    {
        /// <summary>
        /// MonkeyId
        /// </summary>
        [DataMember(Name = "MonkeyId")]
        public Guid MonkeyId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember(Name = "Status")]
        public string? Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [DataMember(Name = "Message")]
        public string? Message { get; set; }

    }
}