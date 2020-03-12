using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectionManager
{
    interface IProjection
    {
        bool CanHandle(string eventType);

        void Handle(string eventType, object e);
    }
}
