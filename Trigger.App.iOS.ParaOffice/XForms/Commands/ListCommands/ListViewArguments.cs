using System;
using Eto.Forms;
using System.Collections.Generic;
using XForms.Store;

namespace XForms.Commands
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
