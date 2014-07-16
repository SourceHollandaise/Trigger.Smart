using Trigger.CRM.Model;

namespace Trigger.CRM.Commands
{
    public class TimeTrackerCommand : ModelCommand<TimeTracker>
    {
        public override string GetRepresentation(TimeTracker item)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("'{0}' by {1} on {2}", item.Subject, item.User));
            if (item.Duration != null)
                sb.AppendLine(string.Format("{0} - {1} with duration {2}", item.Begin, item.End));
            sb.AppendLine(string.Format("Linked to '{0}' project", item.Project));
            sb.AppendLine(string.Format("{0}", item.Description));
            sb.AppendLine(string.Format("ID: {0}", item.MappingId));
            return sb.ToString();
        }
    }
}

