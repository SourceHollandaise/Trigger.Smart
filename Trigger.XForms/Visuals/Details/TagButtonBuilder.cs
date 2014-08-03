using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Commands;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;

namespace Trigger.XForms.Visuals
{
    public class TagButtonBuilder
    {
        readonly IList<Button> tagButtons = new List<Button>();

        readonly IStorable currentObject;

        public TagButtonBuilder(IStorable currentObject)
        {
            this.currentObject = currentObject;
        }

        public void AddTagButtonsContent(DynamicLayout commandBar)
        {
            commandBar.Add(new DynamicLayout(){ Size = new Size(40, -1) });

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
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(currentObject.MappingId.ToString()));
            if (tag != null)
            {
                var tagbutton = tagButtons.FirstOrDefault(p => p.BackgroundColor.Equals(Color.Parse(tag.TagColor)));
                if (tagbutton != null)
                {
                    if (Color.Parse(tag.TagColor) == Colors.WhiteSmoke)
                        return;
                    tagbutton.Image = ImageExtensions.GetImage("Accept24", 24);
                    tagbutton.ImagePosition = ButtonImagePosition.Overlay;
                }
            }
        }

        Button TagButton(Color color)
        {
            var tagButton = new Button
            {
                Size = new Size(40, 40),
                ID = "tag_" + color,
                BackgroundColor = color,
                ToolTip = "Add Tag "
            };

            tagButtons.Add(tagButton);

            tagButton.Click += (sender, e) =>
            {
                var data = Tuple.Create<Button, IList<Button>>(tagButton, tagButtons);
                DependencyMapProvider.Instance.ResolveType<ITagCommand>().Execute(new DetailViewArguments { CurrentObject = currentObject, InputData = data });
            };

            return tagButton;
        }
    }
}