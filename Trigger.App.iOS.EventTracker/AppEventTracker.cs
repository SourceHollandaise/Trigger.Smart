//using GLib;
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;

namespace Trigger.App.EventTracker
{
    public class AppEventTracker : Eto.Forms.Application
    {
        public AppEventTracker(Eto.Platform p) : base(p)
        {
            
        }

        public  void InitalizeApplication()
        {
            var init = new Bootstrapper();
            init.InitalizeDataStore();
            init.RegisterDependencies();
            init.CreateInitialObjects();
            init.RegisterDeclaredTypes();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);



            MainForm = new MainViewTemplate();
            MainForm.BringToFront();
            MainForm.Show();
        }
    }

    public  class MyMain : Eto.Forms.Form
    {
        public MyMain()
        {
            var button = new Button(){ Text = "Click Me for detail!" };

            button.Click += (sender, e) =>
            {
                var detail = new MyDetail();
                detail.Show();
            };
            Content = button;
        }

       
    }


    public class MyDetail : Eto.Forms.Form
    {
        public MyDetail()
        {
            var button = new Button(){ Text = "Click me for main!" };

            button.Click += (sender, e) =>
            {
                this.Close();
                Application.Instance.MainForm.Show();
            };
            Content = button;
        }
    }
      
}
