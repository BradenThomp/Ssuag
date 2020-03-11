﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Handlers
{
    public class CommandHandler
    {
        /// <summary>
        /// Maps the name of the command class being added, to the method that should be executed when the command is called.
        /// This func should take the command object and returns a Task.
        /// </summary>
        internal Dictionary<string, Func<object, Task>> Handlers { get; } = new Dictionary<string, Func<object, Task>>();

        /// <summary>
        /// Adds command, Func pair to dictionary
        /// </summary>
        /// <typeparam name="T">command object</typeparam>
        /// <param name="handler">give a lambda expression for how to handle command</param>
        protected void Register<T>(Func<T, Task> handler)
        {
            Handlers.Add(typeof(T).Name, c => handler((T)c));
        }
    }
}
