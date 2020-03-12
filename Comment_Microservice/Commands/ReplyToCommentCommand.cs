using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
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
    }
}
