using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Comment_Microservice.Commands;
using Comment_Microservice.Aggregates.Repository;
using Comment_Microservice.Events;
using Comment_Microservice.Commands.Handlers;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comment_Microservice.Controllers
{

    [Route("api/[controller]/[action]")]
    public class CommandController : Controller
    {
        private readonly Dispatcher _dispatcher;

        /**
         * On creation opens up a connection to an EventStore Database
         */
        public CommandController()
        {
            var eventStoreConnection = EventStoreConnection.Create(ConnectionSettings.Default, new IPEndPoint(IPAddress.Loopback,1113));

            eventStoreConnection.ConnectAsync();

            //injects the connection into an AggregateRepository
            var repository = new AggregateRepository(eventStoreConnection);

            CommentCommandHandler[] commands = { new CommentCommandHandler(repository) };

            //inject AggregateRepository into a CommentHandlers(event handler) then map all comment commands
            var commandHandlerMap = new CommandHandlerMap(commands);

            //inject mapped commands into dispatcher
            _dispatcher = new Dispatcher(commandHandlerMap);
        }

        /**
         * Route is api/command/createcomment
         * 
         * When a create comment command is recieved - dispatches the command 
         */
        [HttpPost]
        public async Task CreateComment([FromBody] CreateComment createComment) 
        {

            //dispatches a new CreateComment Command
            await _dispatcher.Dispatch(createComment);

        }

        [HttpPost]
        public async Task ReplyToComment([FromBody] ReplyToCommentCommand replyToCommentCommand)
        {
            await _dispatcher.Dispatch(replyToCommentCommand);
        }



    }
}
