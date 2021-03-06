using System.Linq;
using Eto.Forms;
using System;
using System.Collections.Generic;
using XForms.Design;
using XForms.Dependency;
using XForms.Store;
using XForms.Model;
using Eto.Drawing;

namespace XForms.Commands
{
    public class TagDetailViewCommand : ITagDetailViewCommand
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

                var store = MapProvider.Instance.ResolveType<IStore>();
                var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(args.CurrentObject.MappingId.ToString()));
                if (tag == null)
                    tag = new Tag();

                tag.TargetObjectMappingId = args.CurrentObject.MappingId.ToString();
                tag.TagColor = data.Item1.BackgroundColor.ToString();
                //data.Item1.Text = "X";

                data.Item1.Image = ImageExtensions.GetImage("tag", 16);
                data.Item1.ImagePosition = ButtonImagePosition.Above;

                tag.Save();
            }
        }

        public string ID => "cmd_tags";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Tag";

        public string ImageName => "tag";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}
