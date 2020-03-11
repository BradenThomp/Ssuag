using Comment_Microservice.Aggregates;
using Comment_Microservice.Aggregates.Repository;
using Comment_Microservice.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands.Handlers
{
    public class CommentCommandHandler : CommandHandler
    {
        public CommentCommandHandler(AggregateRepository repository)
        {
            //code executes when a CreateComment Command is dispatched
            //Creates a new comment aggregate and sends it to the repository which pushes new events to EventStore
            Register<CreateComment>(async c =>
            {
                var encounter = new CommentAggregate(c.CommentId, c.PostId, c.Content, c.Username);
                await repository.Save(encounter);
            });
        }
    }
}
