using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands.Handlers
{
    /// <summary>
    /// Receives a command, and begins to execute the associated handler
    /// </summary>
    public class Dispatcher
    {
        private readonly CommandHandlerMap _map;

        public Dispatcher(CommandHandlerMap map)
        {
            _map = map;
        }

        public Task Dispatch(object command)
        {
            var handler = _map.Get(command);

            return handler(command);
        }
    }
}
