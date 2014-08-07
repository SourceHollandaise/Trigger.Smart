using System;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.Common.Datastore
{
    public class GuidIdGenerator : IdGenerator
    {
        public object GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
