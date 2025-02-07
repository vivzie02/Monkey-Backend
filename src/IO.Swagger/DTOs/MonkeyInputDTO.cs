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
    public class MonkeyInputDTO
    {
        /// <summary>
        /// MonkeyId
        /// </summary>
        [DataMember(Name = "numberOfMonkeys")]
        public int NumberOfMonkeys { get; set; }

    }
}