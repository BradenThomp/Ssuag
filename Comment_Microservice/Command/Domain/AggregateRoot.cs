using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Domain
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        // how to respond to events for this type of object
        readonly Dictionary<Type, Action<object>> _handlers = new Dictionary<Type, Action<object>>();

        // Events associated with this object, in case replay needed
        readonly List<object> _events = new List<object>();

        public Guid Id { get; protected set; }

        public int Version { get; protected set; } = -1;

        List<object> IAggregateRoot.GetEvents()
        {
            return _events;
        }

        void IAggregateRoot.ClearEvents()
        {
            _events.Clear();
        }

        protected void Register<T>(Action<T> when)
        {
            _handlers.Add(typeof(T), e => when((T)e));
        }

        void IAggregateRoot.Apply(object e)
        {
            Raise(e);
            Version++;
        }

        //Stores an event into the list of events
        protected void Raise(object e)
        {
            _handlers[e.GetType()](e);
            _events.Add(e);
        }
    }
}
