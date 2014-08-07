using System;
using Eto.Forms;
using System.Collections.Generic;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class ListViewArguments
    {
        public GridView Grid
        {
            get;
            set;
        }

        public Type TargetType
        {
            get;
            set;
        }

        public IEnumerable<IStorable> CustomDataSet
        {
            get;
            set;
        }

        public object InputData
        {
            get;
            set;
        }
    }
}
