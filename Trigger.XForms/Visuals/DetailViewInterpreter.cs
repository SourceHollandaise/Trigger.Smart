using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XForms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{

    public class DetailViewInterpreter
    {

        protected ViewDescriptor Descriptor
        {
            get;
            set;
        }

        protected IStorable CurrentObject
        {
            get;
            set;
        }

        public Type CurrentType
        {
            get;
            set;
        }

        protected DetailPropertyEditorFactory ControlFactory
        {
            get;
            set;
        }


        public DetailViewInterpreter(ViewDescriptor descriptor, IStorable currentObject)
        {
            this.Descriptor = descriptor;
            this.CurrentObject = currentObject;
            this.CurrentType = this.CurrentObject.GetType();

            ControlFactory = new DetailPropertyEditorFactory(this.CurrentObject);
        }

        public DynamicLayout CreateFromDescriptor()
        {
            var groups = Descriptor.GroupItems.OrderBy(p => p.Index).ToList();

            var layout = new DynamicLayout();
            foreach (var group in groups)
            { 
                var groupLayout = CreateGroupFromDescriptor(group);
                layout.Add(groupLayout);
 
            }

            layout.BeginVertical();
            layout.EndVertical();

            return layout;
        }

        GroupBox CreateGroupFromDescriptor(GroupItem group)
        {
            var layout = new DynamicLayout();

            layout.BeginVertical();

            var groupBox = new GroupBox();
            groupBox.Text = group.HeaderText;

            foreach (var viewItem in group.ViewItems.OrderBy(p => p.Index).ToList())
            {
                var property = CurrentType.GetProperty(viewItem.FieldName);

                if (property == null)
                    continue;

                var control = ControlFactory.GetControl(property);

                if (control == null)
                    continue;

                if (viewItem.ShowLabel)
                {
                    layout.Add(new Label
                    {
                        Text = viewItem.LabelText
                    });
                }
                layout.Add(control);
            }

            layout.EndVertical();
            groupBox.Content = layout;
            return groupBox;
        }
    }
}