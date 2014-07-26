using System;

namespace Trigger.XStorable.DataStore
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class RuleRequiredFieldAttribute : Attribute
    {

    }
}
