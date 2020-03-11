using Comment_Microservice.Command.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Domain
{
    /// <summary>
    /// Stores all attributes of a comment, and how to respond to Events concerning comments.
    /// </summary>
    public class Comment : AggregateRoot
    {
        int _postId;

        string _content;

        public string _username;

        /*
         * Calls private ctor
         * Raises a new comment created event with the arguments provided
         */
        public Comment(Guid commentId, int postId, string content, string username) : this()
        {
            Raise(new CommentCreated(commentId, postId, content, username));
        }

        /*
         * Tells AggregateRoot which method to call for each Event received.
         */
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
