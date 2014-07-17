
using System;
using Trigger.CRM.Model;
using Trigger.Datastore.Persistent;
using Trigger.Datastore.Security;

namespace Trigger.CommandLine.Commands
{
    public class ConsoleDeleteCommand : ConsoleBaseCommand
    {
        public static void DeleteItem(string target, string id)
        {
            if (target.ToLower().Equals("user"))
            {
                DeleteWithMessage<User>(id);
            }

            if (target.ToLower().Equals("issue"))
            {
                DeleteWithMessage<IssueTracker>(id);
            }

            if (target.ToLower().Equals("project"))
            {
                DeleteWithMessage<Project>(id);
            }

            if (target.ToLower().Equals("times"))
            {
                DeleteWithMessage<TimeTracker>(id);
            }

            if (target.ToLower().Equals("document"))
            {
                DeleteWithMessage<Document>(id);
            }
        }

        static void DeleteWithMessage<T>(object id) where T: IPersistentId
        {
            Console.WriteLine("Delete {0} item...", typeof(T).Name);

            Store.DeleteById<T>(id);

            if (Store.Load<T>(id) == null)
                Console.WriteLine("Item succesful deleted!");
            else
                Console.WriteLine("Error delete item with id {0}", id);
        }
    }
}