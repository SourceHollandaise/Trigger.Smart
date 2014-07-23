

namespace Trigger.XForms.Security
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonParameters);
    }
}
