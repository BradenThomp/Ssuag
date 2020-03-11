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
            //code executes when a CreateComment Command is dispatched
            Register<CreateComment>(async c =>
            {
                var encounter = new Comment(c.CommentId, c.PostId, c.Content, c.Username);
                await repository.Save(encounter);
            });
        }
    }
}
