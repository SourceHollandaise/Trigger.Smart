using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Layout
{
	public class MainForm : Form
	{
		public MainForm(IEnumerable<Type> types)
		{
			Size = new Size(1280, 800);

			ListBox listBox = new ListBox();

			foreach (var type in types)
				listBox.Items.Add(new ListItem{ Text = type.Name, Key = type.FullName, Tag = type });
				
			Content = listBox;

			listBox.KeyDown += (sender, e) =>
			{
				if (e.Key != Keys.Enter)
					return;

				new ModelListForm((listBox.SelectedValue as ListItem).Tag as Type, null).Show();
			};
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			var logon = new LogonForm();
			logon.Topmost = true;

			logon.Show();

		}
	}
}
