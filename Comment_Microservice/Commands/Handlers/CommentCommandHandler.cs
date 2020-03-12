using Comment_Microservice.Aggregates;
using Comment_Microservice.Aggregates.Repository;
using Comment_Microservice.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands.Handlers
{
    /// <summary>
    /// Responsible for storing all code for how commands related to comments should be handled.
    /// </summary>
    public class CommentCommandHandler : CommandHandler
    {
        public CommentCommandHandler(AggregateRepository repository)
        {
            /*
             * Below, see a list of how to respond to commands.
             * Lambda expressions dynamically create methods, which are assigned to the parameterized command
             * Consider the Register methods as separate from the lambda expressions.
             */
            Register<CreateComment>(async c =>
            {
                //code executes when a CreateComment Command is dispatched
                //Creates a new comment aggregate (which creates a new Event in its ctor)
                //sends it to the repository which pushes new events to EventStore
                var newComment = new CommentAggregate(c.CommentId, c.PostId, c.Content, c.Username);
                await repository.Save(newComment);
            });

            Register<ReplyToCommentCommand>(async c =>
                {
                    //code executes when a ReplyToCommentCommand is dispatched
                    //Creates a new comment aggregate (which creates a new Event in its ctor)
                    //sends it to the repository which pushes new events to EventStore
                    var newCommentReply = new CommentAggregate(c.CommentId, c.ParentId, c.PostId, c.Content, c.Username);
                    await repository.Save(newCommentReply);
                }
            );

            Register<EditComment>(async c =>
            {
                //code executes when a EditComment Command is dispatched
                //Pulls a copy of the comment from the repository, applies the new content to it and
                //sends it to the repository which pushes new events to EventStore
                var comment = await repository.Get<CommentAggregate>(c.CommentId);
                comment.EditComment(c.Content);
                await repository.Save(comment);
            });

            Register<DeleteComment>(async c =>
            {
                //code executes when a DeleteComment Command is dispatched
                //Pulls a copy of the comment from the repository, changes the delete flag to true and
                //sends it to the repository which pushes new events to EventStore
                var comment = await repository.Get<CommentAggregate>(c.CommentId);
                comment.DeleteComment();
                await repository.Save(comment);
            });
        }
    }
}
