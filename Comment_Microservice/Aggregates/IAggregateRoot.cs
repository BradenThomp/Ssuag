using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Aggregates
{
    public interface IAggregateRoot
    {
        List<object> GetEvents();

        void ClearEvents();

        void Apply(object e);

        Guid Id { get; }

        int Version { get; }
    }
}
