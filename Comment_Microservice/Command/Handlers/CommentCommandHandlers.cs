using Comment_Microservice.Command;
using Comment_Microservice.Command.Domain.Repository;
using Comment_Microservice.Command.Domain;
using Comment_Microservice.Command.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Events
{
    public class CommentCommandHandlers : CommandHandler
    {
        public CommentCommandHandlers(AggregateRepository repository)
        {
            /*
             * Use lambda expression to dynamically create a method that will be executed when
             * the dispatcher is told to execute that command. 
             */
            Register<CreateComment>(async c =>
            {
                /*
                 * Create new comment and a corresponding event.
                 * Save the new comment to Event Store.
                 */
                var newComment = new Comment(c.CommentId, c.PostId, c.Content, c.Username);
                await repository.Save(newComment);
            });
        }
    }
}
