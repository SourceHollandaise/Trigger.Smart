using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.CRM.Model;

namespace Trigger.CRM.Forms
{

	public class ModelListTemplate : Form
	{
		ListBox listBox;

		public ModelListTemplate()
		{
		
			Size = new Eto.Drawing.Size(640, 480);
			Title = "Documents from store";
			Content = listBox = new ListBox();
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			var map = Trigger.Dependency.DependencyMapProvider.Instance;
			var items = map.ResolveType<IStore>().LoadAll<IssueTracker>();

			foreach (var item in items)
			{
				var li = new ListItem();
				li.Key = item.MappingId.ToString();
				li.Text = item.Subject;

				listBox.Items.Add(li);
			}
		
			listBox.Size = new Eto.Drawing.Size(600, 400);
		}
	}

}
