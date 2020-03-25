using System;
using System.Collections.Generic;
using System.Linq;
using Comment_Microservice.Events;
using _401Project.Models;

namespace ProjectionManager
{
    class CommentProjection : Projection
    {
        public CommentProjection(DBConnection dbConnection)
        {
            Register<CommentCreatedEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    session.Store(new Comment
                    {
                        Id = e.CommentId.ToString(),
                        ParentId = e.CommentId.ToString(),
                        PostId = e.PostId.ToString(),
                        Content = e.Content,
                        UserName = e.Username
                    });

                    session.SaveChanges();
                }

                Console.WriteLine($"Comment created: {e.CommentId}");
            });

            Register<ReplyToCommentEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    session.Store(new Comment
                    {
                        Id = e.CommentId.ToString(),
                        ParentId = e.ParentId.ToString(),
                        PostId = e.PostId.ToString(),
                        Content = e.Content,
                        UserName = e.Username,
                        TimesEdited = 0,
                        Deleted = false
                    });

                    session.SaveChanges();
                }
                Console.WriteLine($"Comment {e.CommentId} replied to comment {e.ParentId}");
            });

            Register<CommentDeletedEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    var comment = session.Load<Comment>(e.CommentId.ToString());

                    comment.Deleted = true;

                    session.SaveChanges();
                }
                Console.WriteLine($"Comment deleted: {e.CommentId}");
            });

            Register<CommentEditedEvent>(e =>
            {
                using (var session = dbConnection.Connect())
                {
                    var comment = session.Load<Comment>(e.CommentId.ToString());

                    comment.Content = e.Content;

                    comment.TimesEdited++;

                    session.SaveChanges();
                }
                Console.WriteLine($"Comment edited: {e.CommentId}");
            });
        }
    }
}
