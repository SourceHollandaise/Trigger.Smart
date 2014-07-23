//using GLib;
using Trigger.CRM.Model;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using Trigger.Datastore.Security;
using System;

namespace Trigger.Application.WinForms
{
	public class CrossPlatformApplication : Eto.Forms.Application
	{
		public virtual void Initialize()
		{
			var init = new ApplicationInitializer();
			init.InitalizeDataStore();
			init.RegisterDependencies();
			init.CreateInitialObjects();
			init.RegisterDeclaredTypes();
		}

		public override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			MainForm = new MainViewTemplate();
			MainForm.BringToFront();
			MainForm.Show();
		}

		public override void OnTerminating(System.ComponentModel.CancelEventArgs e)
		{
			base.OnTerminating(e);
		}
	}
}
