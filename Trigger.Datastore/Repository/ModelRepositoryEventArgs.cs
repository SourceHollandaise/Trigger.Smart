using System;

namespace Trigger.Datastore.Repository
{
    public delegate void RepositoryHandler(object sender,ModelRepositoryEventArgs e);

    public class ModelRepositoryEventArgs: EventArgs
    {
        public ModelRepositoryEventArgs(object item)
        {
            Item = item;
        }

        public object Item { get; private set; }

    }
}
