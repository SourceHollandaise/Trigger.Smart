using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using System.Linq;
using Trigger.WinForms.Actions;
using Trigger.Datastore.Security;

namespace Trigger.WinForms.Layout
{
	public class MainViewTemplate : TemplateBase
	{
		public MainViewTemplate(IEnumerable<Type> types) : base(types.FirstOrDefault(), null)
		{
			Size = new Size(640, 480);

			Content = new MainViewGenerator(types).GetLayout();

			Controllers.Add(new ActionApplicationExitController(this, null));
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

		public override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			var result = MessageBox.Show("Close application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
			if (result == DialogResult.Yes)
			{
				e.Cancel = false;

				var templatesToClose = TemplateManager.ActiveTemplates.ToList();
				while (templatesToClose[0] != null)
				{
					templatesToClose[0].Close();
					templatesToClose[0].Dispose();
				}
					
				Application.Instance.Quit();
			}
			else
				e.Cancel = true;
				
		}
	}
}
