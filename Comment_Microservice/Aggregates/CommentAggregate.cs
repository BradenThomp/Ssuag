using Comment_Microservice.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Aggregates
{
    /// <summary>
    /// Encapsulates all properties of a comment, and the actions to be taken
    /// when an Event is incoming concerning a comment.
    /// </summary>
    public class CommentAggregate : AggregateRoot
    {
        int _postId;

        string _content;

        public string _username;

        /*
         * Calls private ctor first
         * Creates new event, which is then handled by the appropriate method
         */
        public CommentAggregate(Guid commentId, int postId, string content, string username) : this()
        {
            Raise(new CommentCreatedEvent(commentId, postId, content, username));
        }

        /*
         * Tells AggregateRoot which On method to call when event raised.
         */
        private CommentAggregate()
        {
            Register<CommentCreatedEvent>(On);
        }

        private void On(CommentCreatedEvent e)
        {
            Id = e.CommentId;
            _postId = e.PostId;
            _content = e.Content;
            _username = e.Username;
        }
    }
}
