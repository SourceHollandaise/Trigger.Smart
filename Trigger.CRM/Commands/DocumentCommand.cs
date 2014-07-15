using Trigger.CRM.Model;

namespace Trigger.CRM.Commands
{

    public class DocumentCommand : ModelCommand<Document>
    {
        public override string GetRepresentation(Document item)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("'{0}' by {1} on {2}", item.Subject, item.User, item.Created));
            sb.AppendLine(string.Format("Linked to '{0}' project", item.Project));
            sb.AppendLine(string.Format("{0}", item.Description));
            return sb.ToString();
        }
    }
    
}
