namespace Trigger.CRM.Persistent
{
    public class SchroederIdGenerator : IdGenerator
    {
        public object GetId()
        {
            return Schroeder.UniqueId();
        }
    }
}