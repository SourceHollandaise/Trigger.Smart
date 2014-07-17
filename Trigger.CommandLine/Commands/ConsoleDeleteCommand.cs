
using System;
using Trigger.CRM.Model;

namespace Trigger.CommandLine.Commands
{
    public class ConsoleDeleteCommand : ConsoleBaseCommand
    {
        public static void DeleteItem(string target, string id)
        {
            if (target.ToLower().Equals("user"))
            {
                Console.WriteLine("Delete exisiting user...");

                Store.DeleteById(typeof(User), id);
            }

            if (target.ToLower().Equals("issue"))
            {
                Console.WriteLine("Delete exisiting issue...");

                Store.DeleteById(typeof(IssueTracker), id);
            }

            if (target.ToLower().Equals("project"))
            {
                Console.WriteLine("Delete exisiting project...");

                Store.DeleteById(typeof(Project), id);
            }

            if (target.ToLower().Equals("times"))
            {
                Console.WriteLine("Delete exisiting tracked time...");

                Store.DeleteById(typeof(TimeTracker), id);
            }

            if (target.ToLower().Equals("document"))
            {
                Console.WriteLine("Delete exisiting document...");

                Store.DeleteById(typeof(Document), id);

            }
        }
    }
}