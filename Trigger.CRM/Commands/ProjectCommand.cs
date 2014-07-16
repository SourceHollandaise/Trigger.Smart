using Trigger.CRM.Model;
using System.Collections.Generic;
using Trigger.CRM.Persistent;
using System.Linq;

namespace Trigger.CRM.Commands
{

    public class ProjectCommand : ModelCommand<Project>
    {
        public override string GetRepresentation(Project item)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("{0}", item.Name));
            sb.AppendLine(string.Format("{0}", item.Description));
            sb.AppendLine(string.Format("ID: {0}", item.MappingId));
            return sb.ToString();
        }

        public IEnumerable<IssueTracker> GetIssueTrackerLinks(Project item)
        {
            return Map.ResolveType<IStore<IssueTracker>>().LoadAll().Where(p => p.Project != null && p.Project.MappingId == item.MappingId);
        }
    }
}
