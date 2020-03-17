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
using Comment_Microservice.DataTransferObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comment_Microservice.Controllers
{

    [Route("api/[controller]/[action]")]
    public class CommandController : Controller
    {
        private readonly Dispatcher _dispatcher;

        /**
         *
         */
        public CommandController(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /**
         * Route is api/command/createcomment
         * 
         * When a create comment command is recieved - dispatches the command 
         */
        [HttpPost]
        public async Task CreateComment([FromBody] CreateCommentDto data) 
        {
            //dispatches a new CreateComment Command
            CreateCommentCommand command = new CreateCommentCommand(data.Content, data.Username, data.PostId);
            await _dispatcher.Dispatch(command);
        }

        /**
         * Route is api/command/replytocommment
         * 
         * When a create comment command is recieved - dispatches the command 
         */
        [HttpPost]
        public async Task ReplyToComment([FromBody] ReplyToCommentDto data)
        {
            ReplyToCommentCommand command = new ReplyToCommentCommand(data.Content, data.Username, data.PostId, data.ParentId);
            await _dispatcher.Dispatch(command);
        }

        /**
         * Route is api/command/editcomment
         * 
         * When a create comment command is recieved - dispatches the command 
         */
        [HttpPost]
        public async Task EditComment([FromBody] EditCommentDto data)
        {
            await _dispatcher.Dispatch(new EditCommentCommand(data.CommentId, data.Content));
        }

        /**
         * Route is api/command/deletecomment
         * 
         * When a create comment command is recieved - dispatches the command 
         */
        [HttpPost]
        public async Task DeleteComment([FromBody] DeleteCommentDto data)
        {
            await _dispatcher.Dispatch(new DeleteCommentCommand(data.CommentId));
        }

    }
}
