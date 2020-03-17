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

        Guid _parentId;

        Guid _postId;

        string _content;

        int _timesEdited = 0;

        public string _username;

        bool _deleted = false;

        /*
         * Creates a new autonomous comment.
         * Calls private ctor first
         * Creates new event, which is then handled by the appropriate method
         */
        public CommentAggregate(Guid commentId, Guid postId, string content, string username) : this()
        {
            Raise(new CommentCreatedEvent(commentId, postId, content, username));
        }

        /*
         * Creates a new reply to a comment
         */
        public CommentAggregate(Guid commentId, Guid parentId, Guid postId, string content, string username) : this()
        {
            Raise(new ReplyToCommentEvent(commentId, parentId, postId, content, username));
        }

        /*
         * Tells AggregateRoot which On method to call when event raised.
         */
        private CommentAggregate()
        {
            Register<CommentCreatedEvent>(On);
            Register<ReplyToCommentEvent>(On);
            Register<CommentDeletedEvent>(On);
            Register<CommentEditedEvent>(On);
        }

        private void On(CommentCreatedEvent e)
        {
            Id = e.CommentId;
            _postId = e.PostId;
            _content = e.Content;
            _username = e.Username;
        }

        private void On(ReplyToCommentEvent e)
        {
            Id = e.CommentId;
            _parentId = e.ParentId;
            _postId = e.PostId;
            _content = e.Content;
            _username = e.Username;
        }
        /*
         * Called by CommandHandler to change the comment context and raise a new event
         */
        public void EditComment(string content)
        {
            checkIfCommentDeleted();

            Raise(new CommentEditedEvent(Id, content));
        }

        private void On(CommentEditedEvent e)
        {
            _content = e.Content;
            _timesEdited++;
        }

        /*
         * Used to ensure comment hasn't been deleted yet
         */
        private void checkIfCommentDeleted()
        {
            if (_deleted)
            {
                throw new Exception("Comment has already been deleted");
            }
        }

        /*
         * Called by CommandHandler to change the delete flag to true and raise a new event
         */
        public void DeleteComment()
        {
            checkIfCommentDeleted();

            Raise(new CommentDeletedEvent(Id));
        }

        private void On(CommentDeletedEvent e)
        {
            _deleted = true;
        }

    }
}
