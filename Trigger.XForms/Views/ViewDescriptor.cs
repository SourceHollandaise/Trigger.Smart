using System;
using Trigger.XStorable.DataStore;
using System.Collections.Generic;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor
    {
        public IList<GroupItem> GroupItems { get; set; }
    }

    public class GroupItem
    {
        public GroupItem(string headertext, int index)
        {
            HeaderText = headertext;
            Index = index;
        }

        public string HeaderText { get; set; }

        public int Index { get; set; }

        public IList<ViewItem> ViewItems { get; set; }
    }

    public class ViewItem
    {
        public ViewItem(string fieldName, int index)
        {
            FieldName = fieldName;
            Index = index;
        }

        public string FieldName { get; set; }

        public string LabelText { get; set; }

        public bool ShowLabel { get; set; }

        public int Index { get; set; }
    }
}

