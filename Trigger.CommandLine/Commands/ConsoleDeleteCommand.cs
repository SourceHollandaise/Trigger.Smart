
using System;
using Trigger.CRM.Commands;

namespace Trigger.CommandLine.Commands
{
    public class ConsoleDeleteCommand : ConsoleBaseCommand
    {
        public static void DeleteItem(string target, string id)
        {
            if (target.ToLower().Equals("user"))
            {
                var cmd = new UserCommand();

                Console.WriteLine("Delete exisiting user...");

                cmd.Delete(id);
            }

            if (target.ToLower().Equals("issue"))
            {
                var cmd = new IssueTrackerCommand();

                Console.WriteLine("Delete exisiting issue...");

                cmd.Delete(id);
            }

            if (target.ToLower().Equals("project"))
            {
                var cmd = new ProjectCommand();

                Console.WriteLine("Delete exisiting project...");

                cmd.Delete(id);
            }

            if (target.ToLower().Equals("times"))
            {
                var cmd = new TimeTrackerCommand();

                Console.WriteLine("Delete exisiting tracked time...");

                cmd.Delete(id);
            }

            if (target.ToLower().Equals("document"))
            {

                var cmd = new DocumentCommand();

                Console.WriteLine("Delete exisiting document...");

                cmd.Delete(id);

            }
        }
    } 
}