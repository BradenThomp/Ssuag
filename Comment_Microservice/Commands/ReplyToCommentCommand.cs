using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    /// <summary>
    /// Stores all contents of the new reply to be created.
    /// </summary>
    public class ReplyToCommentCommand
    {
        // ID of this comment
        public Guid CommentId { get; set; }

        // ID of comment this comment is replying to
        public Guid ParentId { get; set; }

        // ID of post this comment belongs to
        public Guid PostId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime TimeOfCreation { get; set; }

        public ReplyToCommentCommand(string content, string username, Guid postId, Guid parentId)
        {
            ParentId = parentId;
            PostId = postId;
            Content = content;
            Username = username;
            GenerateCreationTime();
            GenerateId();
        }

        private void GenerateId()
        {
            CommentId = Guid.NewGuid();
        }

        private void GenerateCreationTime()
        {
            TimeOfCreation = DateTime.UtcNow;
        }
    }
}
