using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands
{
    public class CreateCommentCommand
    {

        public Guid CommentId { get; private set; }

        public Guid PostId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime TimeOfCreation { get; private set; }

        public CreateCommentCommand(string content, string username, Guid postId)
        {
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
