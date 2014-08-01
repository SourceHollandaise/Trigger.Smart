using System;
using Eto.Forms;
using System.Collections.Generic;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class DetailViewArguments
    {
        public DetailViewTemplate Template
        {
            get;
            set;
        }

        public IStorable CurrentObject
        {
            get;
            set;
        }
    }
}
