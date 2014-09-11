using XForms.Model;

namespace XForms.Security
{
    public interface ISecurityInfoProvider
    {
        string UserName { get; set; }

        User CurrentUser { get; }
    }
}
