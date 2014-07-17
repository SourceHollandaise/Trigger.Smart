using System;

namespace Trigger.Datastore.Persistent
{
    public class GuidIdGenerator : IdGenerator
    {
        public object GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}