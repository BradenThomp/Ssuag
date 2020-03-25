using System;
using System.Collections.Generic;
using EventStore.ClientAPI;
using EventStore.Projections.Core;

namespace ProjectionManager
{
    class ProjectionManager
    {
        public class ProjectionState
        {
            public string Id { get; set; }

            public long CommitPosition { get; set; }

            public long PreparePosition { get; set; }
        }

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
            foreach (var projection in _projections)
            {
                StartProjection(projection);
            }
        }

        void StartProjection(IProjection projection)
        {
            var checkpoint = GetPosition(projection.GetType());

            _eventStoreConnection.SubscribeToAllFrom(
                checkpoint,
                CatchUpSubscriptionSettings.Default,
                EventAppeared(projection),
                LiveProcessingStarted(projection));
        }

        Action<EventStoreCatchUpSubscription> LiveProcessingStarted(IProjection projection)
        {
            return s => Console.WriteLine($"Projection {projection.GetType().Name} has caught up, live processing started.");
        }

        Action<EventStoreCatchUpSubscription, ResolvedEvent> EventAppeared(IProjection projection)
        {
            return (d, e) =>
            {
                if (projection.CanHandle(e.Event.EventType))
                {
                    return;
                }

                var deserializedEvent = e.OriginalEvent;
                projection.Handle(e.Event.EventType, deserializedEvent);
                UpdatePosition(projection.GetType(), e.OriginalPosition.Value);
            };
        }

        Position? GetPosition(Type projection)
        {
            using (var session = _dbConnection.Connect())
            {
                var state = session.Load<ProjectionState>(projection.Name);

                if (state == null)
                {
                    return null;
                }
                return new Position(state.CommitPosition, state.PreparePosition);
            }
            
        }

        void UpdatePosition(Type projection, Position position)
        {
            using (var session = _dbConnection.Connect())
            {
                var state = session.Load<ProjectionState>(projection.Name);
                if (state == null)
                {
                    session.Store(new ProjectionState
                    {
                        Id = projection.Name,
                        CommitPosition = position.CommitPosition,
                        PreparePosition = position.PreparePosition
                    });
                }
                else
                {
                    state.CommitPosition = position.CommitPosition;
                    state.PreparePosition = position.PreparePosition;
                }
                session.SaveChanges();
            }
        }
    }
}
