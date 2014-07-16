using Trigger.CRM.Model;

namespace Trigger.CRM.Commands
{
    public class IssueTrackerCommand : ModelCommand<IssueTracker>
    {
        public override string GetRepresentation(IssueTracker item)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("'{0}' by {1} on {2}", item.Subject, item.CreatedBy, item.Created));
            sb.AppendLine(string.Format("{0} is {1}", item.Issue, item.State));
            sb.AppendLine(string.Format("Linked to '{0}' project", item.Project));
            sb.AppendLine(string.Format("{0}", item.Description));
            sb.AppendLine(string.Format("ID: {0}", item.MappingId));
            return sb.ToString();
        }
    }
}
