using System;
using System.Collections.Generic;
using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace ProjectionManager
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = GetEventStoreConnection();

            var dbConnection = new RavenDBConnection("CommentMicroservice");

            var projections = new List<IProjection>
            {
                new CommentProjection(dbConnection)
            };

            var projectionManager = new ProjectionManager(
                eventStoreConnection,
                dbConnection,
                projections);

            projectionManager.Start();

            Console.WriteLine("Projection Manager Running");
            Console.ReadLine();
        }

        static IEventStoreConnection GetEventStoreConnection()
        {
            ConnectionSettings settings = ConnectionSettings.Create()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            var eventStoreConnection = EventStoreConnection.Create(
                settings,
                new IPEndPoint(IPAddress.Loopback, 1113));

            eventStoreConnection.ConnectAsync().Wait();
            return eventStoreConnection;
        }
    }
}