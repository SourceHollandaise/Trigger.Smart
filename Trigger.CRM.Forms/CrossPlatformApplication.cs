//using GLib;
using System;
using Trigger.WinForms.Layout;
using Eto.Misc;

namespace Trigger.Application.WinForms
{
	public class CrossPlatformApplication : Eto.Forms.Application
	{
		public virtual void InitalizeApplication()
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
	}
}
