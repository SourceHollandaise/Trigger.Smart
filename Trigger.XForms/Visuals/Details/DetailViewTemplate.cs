using System;
using Trigger.XStorable.DataStore;
using System.ComponentModel;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms.Visuals
{

    public class DetailViewTemplate : TemplateBase
    {
        public DetailViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            SetContent(currentObject);

            SetSize();

            SetTitle();

            this.BackgroundColor = Colors.WhiteSmoke;
        }

        public void SetContent(IStorable currentObject)
        {
            var descriptorType = DetailViewDescriptorProvider.GetDescriptor(currentObject.GetType());
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IDetailViewDescriptor;
                Content = new DetailViewBuilder(descriptor, currentObject).GetContent();
            }
        }

        void SetSize()
        {
            Size = ViewTemplateConfig.IsCompactViewMode ? ViewTemplateConfig.DetailViewCompactSize : ViewTemplateConfig.DetailViewDefaultSize;
        }

        void SetTitle()
        {
            var displayNameAttribute = ModelType.FindAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null)
                Title = displayNameAttribute.DisplayName + " - " + CurrentObject.GetDefaultPropertyValue();
            else
                Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();
                
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Application & e.Key == Keys.W)
            {
                DependencyMapProvider.Instance.ResolveType<ICloseDetailViewCommand>().Execute(new DetailViewArguments{ CurrentObject = CurrentObject });
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
