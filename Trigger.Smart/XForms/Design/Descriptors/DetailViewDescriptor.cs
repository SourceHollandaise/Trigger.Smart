using System.Collections.Generic;
using System;
using XForms.Dependency;
using XForms.Commands;

namespace XForms.Design
{
    public abstract class DetailViewDescriptor<TModel> : IDetailViewDescriptor
    {
        public bool IsTaggable { get; set; }

        public bool AutoSave { get; set; }

        public int? MinHeight { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<IDetailViewCommand> Commands { get; set; }

        public void RegisterCommands<TCommand>() where  TCommand: IDetailViewCommand
        {
            if (Commands == null)
                Commands = new List<IDetailViewCommand>();

            var command = MapProvider.Instance.ResolveType<TCommand>();

            Commands.Add(command);
        }

        protected FieldNames<TModel> Fields = new FieldNames<TModel>();

        protected const string EmptySpaceFieldName = "EmptySpace";

        protected DetailViewDescriptor()
        {
            RegisterCommands<INavigateBackDetailViewCommand>();
            RegisterCommands<ISaveObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();
           
            IsTaggable = true;
        }
    }
}

