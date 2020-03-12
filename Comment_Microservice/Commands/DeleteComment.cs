using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    /// <summary>
    /// Tells microservice which comment to delete
    /// </summary>
    public class DeleteComment
    {
        public Guid CommentId { get; }
    }
}
