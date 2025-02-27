using System;
using System.Runtime.Serialization;

namespace IO.Swagger.DTOs
{
    public class BookOutputDTO
    {
        /// <summary>
        /// Gets or Sets Content
        /// </summary>

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or Sets Content
        /// </summary>

        [DataMember(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfWords
        /// </summary>

        [DataMember(Name = "numberOfWords")]
        public int NumberOfWords { get; set; }
    }
}
