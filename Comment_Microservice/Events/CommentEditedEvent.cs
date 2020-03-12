using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Events
{
    public class CommentEditedEvent
    {
        public CommentEditedEvent(Guid commentId, string content)
        {
            CommentId = commentId;
            Content = content;
        }

        public Guid CommentId { get; }

        public string Content { get; }

    }
}
