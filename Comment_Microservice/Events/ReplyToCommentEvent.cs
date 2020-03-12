using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Events
{
    public class ReplyToCommentEvent
    {
        public Guid CommentId { get; }

        public Guid ParentId { get; }

        public Guid PostId { get; }

        public string Content { get; }

        public string Username { get; }
        public ReplyToCommentEvent(Guid commentId, Guid parentId, Guid postId, string content, string username)
        {
            CommentId = commentId;
            ParentId = parentId;
            PostId = postId;
            Content = content;
            Username = username;
        }
    }
}
