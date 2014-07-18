using System;
using Eto.Forms;
using Trigger.CRM.Model;

namespace Trigger.CRM.Forms.Templates
{

	public class MainForm : Form
	{
		public MainForm()
		{
			Size = new Eto.Drawing.Size(1280, 800);

			ListBox listBox = new ListBox();

			listBox.Items.Add(new ListItem{ Text = "Issues", Key = "Issue", Tag = typeof(IssueTracker) });
			listBox.Items.Add(new ListItem{ Text = "Projects", Key = "Project", Tag = typeof(Project) });
			listBox.Items.Add(new ListItem{ Text = "Documents", Key = "Document", Tag = typeof(Document) });
			listBox.Items.Add(new ListItem{ Text = "Tracked times", Key = "TimeTracker", Tag = typeof(TimeTracker) });

			Content = listBox;

			listBox.KeyDown += (sender, e) =>
			{
				if (e.Key != Keys.Enter)
					return;

				var listTemplate = new ModelListTemplate((listBox.SelectedValue as ListItem).Tag as Type);
				listTemplate.Show();

			};
		}
	}
}
