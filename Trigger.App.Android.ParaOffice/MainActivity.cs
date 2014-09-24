﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XForms.Security;
using System.Diagnostics;

namespace Trigger.App.Android.ParaOffice
{
    [Activity(Label = "Trigger.App.Android.ParaOffice", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            /*
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
			
            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };
            */

            var application = new XForms.Platform.XFormsApplication();

            var logon = new LogonParameters()
            {
                UserName = "JE",
                Password = ""
            };

            application.InitalizeApplication(new AndroidBootstrapper(), Debugger.IsAttached ? logon : null);
            application.Run();
        }
    }
}


