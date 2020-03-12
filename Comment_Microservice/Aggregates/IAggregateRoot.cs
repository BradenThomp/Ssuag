using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Aggregates
{
    /// <summary>
    /// Methods to manipulate Events that apply to an aggregate object
    /// </summary>
    public interface IAggregateRoot
    {
        List<object> GetEvents();

        void ClearEvents();

        void Apply(object e);

        Guid Id { get; }

        /// <summary>
        /// Next valid int value. Prevents race conditions.
        /// </summary>
        int Version { get; }
    }
}
