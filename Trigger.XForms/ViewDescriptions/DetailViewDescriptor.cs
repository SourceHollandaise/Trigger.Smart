using System.Collections.Generic;
using System;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{
    public abstract class DetailViewDescriptor<TModel> : IDetailViewDescriptor
    {
        public bool IsTaggable { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<IDetailViewCommand> Commands { get; set; }

        public void RegisterCommands<TCommand>() where  TCommand: IDetailViewCommand
        {
            if (Commands == null)
                Commands = new List<IDetailViewCommand>();

            var command = DependencyMapProvider.Instance.ResolveType<TCommand>();

            Commands.Add(command);
        }

        protected FieldNames<TModel> Fields = new FieldNames<TModel>();

        protected const string EmptySpaceFieldName = "EmptySpace";

        protected DetailViewDescriptor()
        {
            RegisterCommands<ICloseWindowCommand>();
            RegisterCommands<IDeleteObjectCommand>();
            RegisterCommands<ISaveObjectCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            IsTaggable = true;
        }
    }
}

