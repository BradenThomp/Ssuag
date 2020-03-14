using System;
using System.Collections.Generic;
using EventStore.ClientAPI;

namespace ProjectionManager
{
    class ProjectionManager
    {
        readonly IEventStoreConnection _eventStoreConnection;
        readonly List<IProjection> _projections;
        readonly DBConnection _dbConnection;

        public ProjectionManager(
            IEventStoreConnection eventStoreConnection,
            List<IProjection> projections,
            DBConnection dbConnection)
        {
            _eventStoreConnection = eventStoreConnection;
            _projections = projections;
            _dbConnection = dbConnection;
        }

        public void Start()
        {

        }
    }
}
