
using Trigger.XForms.Commands;
using Trigger.XStorable.Dependency;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;
using System;
using System.Collections.Generic;

namespace Trigger.XForms.Commands
{

    public class TagCommand : ITagCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var data = args.InputData as Tuple<Button, IList<Button>>;

            args.CurrentObject.Save();
            var template = args.CurrentObject.TryGetDetailView();
            if (template != null)
            {
                foreach (var button in data.Item2)
                    button.Image = null;

                var store = DependencyMapProvider.Instance.ResolveType<IStore>();
                var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(args.CurrentObject.MappingId.ToString()));
                if (tag == null)
                    tag = new Tag();

                tag.TargetObjectMappingId = args.CurrentObject.MappingId.ToString();
                tag.TagColor = data.Item1.BackgroundColor.ToString();

                data.Item1.Image = ImageExtensions.GetImage("Accept24", 24);
                data.Item1.ImagePosition = ButtonImagePosition.Overlay;

                tag.Save();
            }
        }

        public string ID
        {
            get
            {
                return "cmd_tags";
            }
        }

        public string Name
        {
            get
            {
                return "Tag";
            }
        }

        public string ImageName
        {
            get
            {
                return "favorite";
            }
        }
    }
}
