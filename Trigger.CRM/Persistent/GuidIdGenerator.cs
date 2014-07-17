using System;

namespace Trigger.CRM.Persistent
{
    public class GuidIdGenerator : IdGenerator
    {
        public object GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
