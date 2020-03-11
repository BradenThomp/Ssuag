using Comment_Microservice.Command;
using Comment_Microservice.Command.Handlers;
using Comment_Microservice.Command.Store;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MicroserviceTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            var dispatcher = await SetupDispatcher();

            var commentId = Guid.NewGuid();

            var createComment = new CreateComment{
                CommentId = commentId, 
                PostId = 10, 
                Content = "comment", 
                Username = "Vector_Bubbs", 
                TimeOfCreation = DateTime.UtcNow 
            };
            //await dispatcher.Dispatch(createComment);

            Console.WriteLine(JsonConvert.SerializeObject(createComment));

            Console.ReadLine();
        }

        private static async Task<Dispatcher> SetupDispatcher()
        {
            var eventStoreConnection = EventStoreConnection.Create(
                ConnectionSettings.Default,
                new IPEndPoint(IPAddress.Loopback, 1113));

            await eventStoreConnection.ConnectAsync();
            var repository = new AggregateRepository(eventStoreConnection);

            var commandHandlerMap = new CommandHandlerMap(new CommentHandlers(repository));

            return new Dispatcher(commandHandlerMap);

        }
    }
}
