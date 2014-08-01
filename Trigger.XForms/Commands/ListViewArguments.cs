using System;
using Eto.Forms;
using System.Collections.Generic;
using Trigger.XStorable.DataStore;

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
    }
}
