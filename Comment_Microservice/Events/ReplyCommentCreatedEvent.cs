using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Events
{
    public class ReplyCommentCreatedEvent
    {
        public ReplyCommentCreatedEvent(Guid commentId, Guid parentID, Guid postId, string content, string username)
        {
            CommentId = commentId;
            ParentID = parentID;
            PostId = postId;
            Content = content;
            Username = username;
        }

        public Guid CommentId { get; }

        public Guid ParentID { get; }

        public Guid PostId { get; }

        public string Content { get; }

        public string Username { get; }

    }
}
