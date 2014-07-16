using Trigger.CRM.Persistent;
using System.Collections.Generic;
using System.Linq;
using Trigger.CRM.Model;

namespace Trigger.CRM.Commands
{

    public class UserCommand : ModelCommand<User>
    {
        public override string GetRepresentation(User item)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("{0}", item.UserName));
            sb.AppendLine(string.Format("{0}", item.EMail));
            sb.AppendLine(string.Format("ID: {0}", item.MappingId));
            return sb.ToString();
        }

        public IEnumerable<Document> GetDocumentLinks(User item)
        {
            return Map.ResolveType<IStore<Document>>().LoadAll().Where(p => p.User.MappingId == item.MappingId);
        }

        public IEnumerable<IssueTracker> GetIssueTrackerLinks(User item)
        {
            return Map.ResolveType<IStore<IssueTracker>>().LoadAll().Where(p => p.CreatedBy.MappingId == item.MappingId);
        }

        public IEnumerable<TimeTracker> GetTimeTrackerLinks(User item)
        {
            return Map.ResolveType<IStore<TimeTracker>>().LoadAll().Where(p => p.User.MappingId == item.MappingId);
        }
    }
    
}
