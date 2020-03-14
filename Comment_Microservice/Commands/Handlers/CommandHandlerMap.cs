using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Commands.Handlers
{
    /// <summary>
    /// Master list of all commands and their tasks
    /// </summary>
    public class CommandHandlerMap
    {
        private readonly Dictionary<string, Func<object, Task>> _handlers = new Dictionary<string, Func<object, Task>>();

        /// <summary>
        /// Takes every unique command, function pair from each CommandHandler, and adds it to the master set.
        /// </summary>
        /// <param name="commandHandlers">accepts a list of parameters that is converted into an array</param>
        public CommandHandlerMap(params CommandHandler[] commandHandlers)
        {
            foreach (var handler in commandHandlers.SelectMany(h => h.Handlers))
            {
                _handlers.Add(handler.Key, handler.Value);
            }
        }

        /// <summary>
        /// Returns the function to be called when the argument command is dispatched.
        /// </summary>
        /// <param name="command">Command searched for.</param>
        /// <returns>Lambda expression for the command, returning a task</returns>
        public Func<object, Task> Get(object command)
        {
            return _handlers[command.GetType().Name];
        }
    }
}
