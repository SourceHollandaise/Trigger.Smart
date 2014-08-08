using System.Linq;
using Eto.Forms;
using System;
using System.Collections.Generic;
using XForms.Design;
using XForms.Dependency;
using XForms.Store;
using XForms.Model;

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
                    button.Text = "";

                var store = MapProvider.Instance.ResolveType<IStore>();
                var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(args.CurrentObject.MappingId.ToString()));
                if (tag == null)
                    tag = new Tag();

                tag.TargetObjectMappingId = args.CurrentObject.MappingId.ToString();
                tag.TagColor = data.Item1.BackgroundColor.ToString();
                data.Item1.Text = "X";

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

        public int Width
        {
            get
            {
                return 36;
            }
        }

        public bool AllowExecute
        {
            get
            {
                return true;
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }
        }
    }
}
