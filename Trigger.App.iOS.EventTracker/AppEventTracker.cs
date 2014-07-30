//using GLib;
using System;
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
           
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
    }

    public class StartUp
    {
        [STAThread]
        public static void Main()
        {

        }
    }
}
