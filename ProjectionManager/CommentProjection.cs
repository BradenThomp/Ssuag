using System;
using System.Collections.Generic;
using System.Linq;
using _401Project.Models;
using Comment_Microservice.Events;

namespace ProjectionManager
{
    internal class CommentProjection : Projection
    {
        public CommentProjection(RavenDBConnection dbConnection)
        {
            When<CommentCreatedEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    session.Store(new Comment
                    {
                        Id = e.CommentId.ToString(),
                        PostId = e.PostId.ToString(),
                        ParentId = null,
                        Content = e.Content,
                        UserName = e.Username
                    });

                    session.SaveChanges();
                }

                Console.WriteLine($"Comment created: {e.CommentId}");
            });

            When<ReplyToCommentEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    session.Store(new Comment
                    {
                        Id = e.CommentId.ToString(),
                        PostId = e.PostId.ToString(),
                        ParentId = e.ParentId.ToString(),
                        Content = e.Content,
                        UserName = e.Username
                    });

                    session.SaveChanges();
                }
                Console.WriteLine($"Comment {e.CommentId} replied to comment {e.ParentId}");
            });

            When<CommentDeletedEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    var comment = session.Load<Comment>(e.CommentId.ToString());

                    session.Delete(comment);

                    session.SaveChanges();
                }
                Console.WriteLine($"Comment deleted: {e.CommentId}");
            });

            When<CommentEditedEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    var comment = session.Load<Comment>(e.CommentId.ToString());

                    comment.Content = e.Content;

                    session.SaveChanges();
                }
                Console.WriteLine($"Comment edited: {e.CommentId}");
            });
        }
    }
}
