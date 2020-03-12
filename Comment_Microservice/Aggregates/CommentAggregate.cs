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
        Guid _commentId;

        Guid _parentId;

        Guid _postId;

        string _content;

        int _timesEdited = 0;

        public string _username;

        bool _deleted = false;

        /*
         * Calls private ctor first
         * Creates new event, which is then handled by the appropriate method
         */
        public CommentAggregate(Guid commentId, Guid postId, string content, string username) : this()
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
            _commentId = e.CommentId;
            _postId = e.PostId;
            _content = e.Content;
            _username = e.Username;
        }
    }
}
