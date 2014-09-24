using System;

namespace XForms.Store
{
    public class GuidIdGenerator : IMappingIdGenerator
    {
        public object GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
