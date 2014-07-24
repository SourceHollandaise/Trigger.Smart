

namespace Trigger.XStorable.Security
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonParameters);
    }
}
