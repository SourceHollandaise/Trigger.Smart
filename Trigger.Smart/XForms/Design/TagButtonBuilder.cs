using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Commands;
using XForms.Dependency;
using XForms.Model;
using XForms.Store;

namespace XForms.Design
{
    public class TagButtonBuilder
    {
        readonly IList<Button> tagButtons = new List<Button>();

        readonly IStorable currentObject;

        public TagButtonBuilder(IStorable currentObject)
        {
            this.currentObject = currentObject;
        }

        public DynamicLayout GetContent()
        {
            var layout = new DynamicLayout();
            layout.BeginHorizontal();
 
            layout.Add(TagButton(Colors.OrangeRed), false, false);
            layout.Add(TagButton(Colors.Orange), false, false);
            layout.Add(TagButton(Colors.YellowGreen), false, false);
            layout.Add(TagButton(Colors.LightSkyBlue), false, false);
            layout.Add(TagButton(Colors.WhiteSmoke), false, false);

            layout.Add(new DynamicLayout(){ Size = new Size(-1, -1) });

            if (currentObject != null && currentObject.MappingId != null)
                UpdateTagButtonAfterCreation();

            return layout;
        }


        public void AddTagButtonsContent(DynamicLayout commandBar, int? spacing = null)
        {
            commandBar.Add(new DynamicLayout(){ Size = new Size(spacing.HasValue ? spacing.Value : 40, -1) });

            commandBar.Add(TagButton(Colors.OrangeRed), false, false);
            commandBar.Add(TagButton(Colors.Orange), false, false);
            commandBar.Add(TagButton(Colors.YellowGreen), false, false);
            commandBar.Add(TagButton(Colors.LightSkyBlue), false, false);
            commandBar.Add(TagButton(Colors.WhiteSmoke), false, false);

            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });

            if (currentObject != null && currentObject.MappingId != null)
                UpdateTagButtonAfterCreation();
        }

        void UpdateTagButtonAfterCreation()
        {
            var store = MapProvider.Instance.ResolveType<IStore>();
            var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(currentObject.MappingId.ToString()));
            if (tag != null)
            {
                var tagbutton = tagButtons.FirstOrDefault(p => p.BackgroundColor.Equals(Color.Parse(tag.TagColor)));
                if (tagbutton != null)
                {
                    if (Color.Parse(tag.TagColor) == Colors.WhiteSmoke)
                        return;

                    tagbutton.Text = "X";
                }
            }
        }

        Button TagButton(Color color)
        {
            var tagButton = new Button
            {
                Size = new Size(34, 34),
                ID = "tag_" + color,
                BackgroundColor = color,
                ToolTip = "Add Tag "
            };

            tagButtons.Add(tagButton);

            tagButton.Click += (sender, e) =>
            {
                var data = Tuple.Create<Button, IList<Button>>(tagButton, tagButtons);
                MapProvider.Instance.ResolveType<ITagDetailViewCommand>().Execute(new DetailViewArguments { CurrentObject = currentObject, InputData = data });
            };

            return tagButton;
        }
    }
}