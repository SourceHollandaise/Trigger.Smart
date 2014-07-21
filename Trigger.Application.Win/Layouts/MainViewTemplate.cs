using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public class MainViewTemplate : Form
	{
		public MainViewTemplate(IEnumerable<Type> types)
		{
			Size = new Size(640, 480);

			ListBox listBox = new ListBox();

			foreach (var type in types)
				listBox.Items.Add(new ListItem{ Text = type.Name, Key = type.FullName, Tag = type });
				
			Content = listBox;

			listBox.KeyDown += (sender, e) =>
			{
				if (e.Key != Keys.Enter)
					return;

				var type = (listBox.SelectedValue as ListItem).Tag as Type;

				var listTemplate = TemplateManager.GetListTemplate(type);
				if (listTemplate.Visible)
					listTemplate.BringToFront();
				else
					listTemplate.Show();
			};
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			var logon = new LogonViewTemplate();
			logon.Topmost = true;

			logon.Show();

		}
	}
}
