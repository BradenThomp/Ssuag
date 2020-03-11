using Comment_Microservice.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Aggregates
{
    public class CommentAggregate : AggregateRoot
    {
        int _postId;

        string _content;

        public string _username;

        //Raises a new comment created event
        public CommentAggregate(Guid commentId, int postId, string content, string username) : this()
        {
            Raise(new CommentCreatedEvent(commentId, postId, content, username));
        }

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
