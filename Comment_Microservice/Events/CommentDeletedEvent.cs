using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Events
{
    public class CommentDeletedEvent
    {
        public CommentDeletedEvent(Guid commentId)
        {
            CommentId = commentId;
        }

        public Guid CommentId { get; }
    }
}
