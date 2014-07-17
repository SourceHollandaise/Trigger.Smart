

namespace Trigger.Datastore.Security
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonParameters);
    }
}
