using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using System.Linq;
using Trigger.WinForms.Actions;
using Trigger.Datastore.Security;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public class MainViewTemplate : TemplateBase
	{
		public MainViewTemplate() : base(typeof(IStorable), null)
		{
			Size = new Size(400, 800);

			Content = new MainViewGenerator(ModelTypesDeclaration.DeclaredModelTypes).GetContent();
			Content = new Scrollable{ Content = this.Content };
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			var logon = new LogonViewTemplate();
			logon.Topmost = true;
			logon.Focus();
			logon.Show();

			logon.Closed += (sender, args) =>
			{
				Title = "User: " + Dependency.DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName;
			};
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control & e.Key == Keys.W)
			{
				var result = MessageBox.Show("Close application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					Application.Instance.Quit();
			}
		}

		public override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);

			var templatesToClose = WindowManager.ActiveViews.ToList();
			while (templatesToClose.Count > 0)
			{
				templatesToClose[0].Close();
				templatesToClose[0].Dispose();
			}
		}
	}
}
