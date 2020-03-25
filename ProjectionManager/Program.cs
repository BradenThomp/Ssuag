﻿using System;
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
            var dbConnection = new DBConnection("CommentDatabase");

            var projections = new List<IProjection>
            {
                new CommentProjection(dbConnection)
            };

            var projectionManager = new ProjectionManager(
                eventStoreConnection,
                projections,
                dbConnection);

            projectionManager.Start();

            Console.WriteLine("Projection Manager Running");
            Console.ReadLine();
        }
        
        private static IEventStoreConnection GetEventStoreConnection()
        {
            ConnectionSettings settings = ConnectionSettings.Create()
                .SetDefaultUserCredentials(new UserCredentials("TODO", "TODO"));

            var eventStoreConnection = EventStoreConnection.Create(
                settings,
                new IPEndPoint(IPAddress.Loopback, 1113));

            eventStoreConnection.ConnectAsync().Wait();

            return eventStoreConnection;
        }
    }
}
