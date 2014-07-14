using System;

namespace Trigger.CRM.Persistent
{
    public class GuidIdGenerator : IdGenerator
    {
        public string GetId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
