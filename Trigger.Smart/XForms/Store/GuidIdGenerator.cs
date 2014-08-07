using System;

namespace XForms.Store
{
    public class GuidIdGenerator : IdGenerator
    {
        public object GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
