
namespace Trigger.CRM.Security
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonparamters);
    }
}
