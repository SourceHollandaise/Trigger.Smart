
using System;
using Trigger.App.ParaOffice;

namespace Trigger.App.OSX.ParaOffice
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var application = new AppParaOffice();

            application.InitalizeApplication();
            application.Run();
        }
    }
}
