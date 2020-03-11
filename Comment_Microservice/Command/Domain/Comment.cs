using Comment_Microservice.Command.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Domain
{
    public class Comment : AggregateRoot
    {
        int _postId;

        string _content;

        public string _username;

        //Raises a new comment created event
        public Comment(Guid commentId, int postId, string content, string username) : this()
        {
            Raise(new CommentCreated(commentId, postId, content, username));
        }

        private Comment()
        {
            Register<CommentCreated>(When);
        }

        private void When(CommentCreated e)
        {
            Id = e.CommentId;
            _postId = e.PostId;
            _content = e.Content;
            _username = e.Username;
        }
    }
}
