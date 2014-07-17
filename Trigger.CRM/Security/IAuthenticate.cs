using Trigger.CRM.Model;


namespace Trigger.CRM.Security
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonParameters);
    }
}
