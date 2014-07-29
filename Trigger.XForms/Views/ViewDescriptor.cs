using System.Collections.Generic;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor
    {
        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }
    }

    public class GroupItemDescription
    {
        public GroupItemDescription(string headertext, int index)
        {
            HeaderText = headertext;
            Index = index;
        }

        public string HeaderText { get; set; }

        public int Index { get; set; }

        public IList<ViewItemDescription> ViewItemDescriptions { get; set; }
    }

    public class ViewItemDescription
    {
        public ViewItemDescription(string fieldName, int index)
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

