using Trigger.CRM.Model;

namespace Trigger.CRM.Commands
{

    public class ProjectCommand : ModelCommand<Project>
    {
        public override string GetRepresentation(Project item)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("{0}", item.Name));
            sb.AppendLine(string.Format("{0}", item.Description));
            return sb.ToString();
        }
    }
    
}
