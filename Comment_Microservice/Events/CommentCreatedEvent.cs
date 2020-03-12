using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Events
{
    /// <summary>
    /// Event to create a new standalone comment.
    /// </summary>
    public class CommentCreatedEvent
    {
        public CommentCreatedEvent(Guid commentId, Guid postId, string content, string username)
        {
            CommentId = commentId;
            PostId = postId;
            Content = content;
            Username = username;
        }

        public Guid CommentId { get; }

        public Guid PostId { get; }

        public string Content { get; }

        public string Username { get; }

    }
}
