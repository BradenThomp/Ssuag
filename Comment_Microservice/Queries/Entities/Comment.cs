using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Queries.Entities
{
    public class Comment
    {
        /**
         * RavenDB does not support Guid's so all Guid's must be type casted to strings
         */
        public string Id { get; set; }

        public string PostId { get; set; }

        public string? ParentId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}
