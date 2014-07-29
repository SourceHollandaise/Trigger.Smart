using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Forms;
using Trigger.XForms;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Visuals
{
    public class ModelToDetailViewInterpreter
    {
        readonly DetailViewControlFactory controlFactory;

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

        public ModelToDetailViewInterpreter(ViewDescriptor descriptor, IStorable currentObject)
        {
            this.Descriptor = descriptor;
            this.CurrentObject = currentObject;
            this.CurrentType = this.CurrentObject.GetType();

            controlFactory = new DetailViewControlFactory(this.CurrentObject);
        }

        public DynamicLayout GetContent()
        {
            return CreateViewLayout();
        }

        DynamicLayout CreateViewLayout()
        {
            var groups = Descriptor.GroupItemDescriptions.OrderBy(p => p.Index).ToList();

            var layout = new DynamicLayout();
            foreach (var group in groups)
            { 
                var groupLayout = CreateGroupLayout(group);
                layout.Add(groupLayout);
            }

            layout.BeginVertical();
            layout.EndVertical();

            return layout;
        }

        GroupBox CreateGroupLayout(GroupItemDescription group)
        {
            var layout = new DynamicLayout();

            layout.BeginVertical();

            var groupBox = new GroupBox();
            groupBox.Text = group.HeaderText;

            foreach (var viewItem in group.ViewItemDescriptions.OrderBy(p => p.Index).ToList())
            {
                var property = CurrentType.GetProperty(viewItem.FieldName);

                if (property == null)
                    continue;

                var control = controlFactory.GetControl(property);

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