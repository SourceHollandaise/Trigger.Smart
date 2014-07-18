using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.Application.Win.Templates
{
	public class MainTemplate : Form
	{
		public MainTemplate(IEnumerable<Type> types)
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

				new ModelListTemplate((listBox.SelectedValue as ListItem).Tag as Type).Show();
			};
		}
	}
}
