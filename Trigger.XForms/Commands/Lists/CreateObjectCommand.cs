using Eto.Forms;
using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class CreateObjectCommand : ICreateObjectCommand
    {
        public void Execute(Type type)
        {
            var target = Activator.CreateInstance(type) as IStorable;
            target.Initialize();

            var detailForm = new DetailViewTemplate(type, target);
            detailForm.Show();
        }

        public string ID
        {
            get
            {
                return "cmd_create";
            }
        }

        public string Name
        {
            get
            {
                return "New";
            }
        }

        public string ImageName
        {
            get
            {
                return "Add16";
            }
        }
    }
}
