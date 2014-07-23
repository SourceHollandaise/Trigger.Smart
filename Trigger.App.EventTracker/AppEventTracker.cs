//using GLib;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.App.EventTracker
{
	public class AppEventTracker : Eto.Forms.Application
	{
		public virtual void InitalizeApplication()
		{
			var init = new Bootstrapper();
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
	}
}