using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectionManager
{
    class EventHandler
    {
        public string EventType { get; set; }

        public Action<object> Handler { get; set; }
    }
}
