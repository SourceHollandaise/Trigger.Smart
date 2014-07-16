
using System;
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.CRM.Commands;

namespace Trigger.CommandLine
{
    public class ConsoleSelectCommand : ConsoleCommand
    {
        public static void ListItems(string target)
        {
            if (target.ToLower().Equals("user"))
            {
                var cmd = new UserCommand();

                Console.WriteLine("Load exisiting users...");
                Console.WriteLine();
                foreach (var item in cmd.GetObjects().OrderBy( p => p.UserName))
                    Console.WriteLine(cmd.GetRepresentation(item));

            }

            if (target.ToLower().Equals("issue"))
            {
                var cmd = new IssueTrackerCommand();

                Console.WriteLine("Load exisiting issues...");
                Console.WriteLine();
                foreach (var item in cmd.GetObjects().OrderBy( p => p.Created))
                    Console.WriteLine(cmd.GetRepresentation(item));

            }

            if (target.ToLower().Equals("project"))
            {
                var cmd = new ProjectCommand();

                Console.WriteLine("Load exisiting projects...");
                Console.WriteLine();
                foreach (var item in cmd.GetObjects().OrderBy(p => p.Name))
                    Console.WriteLine(cmd.GetRepresentation(item));
            }

            if (target.ToLower().Equals("times"))
            {
                var cmd = new TimeTrackerCommand();

                Console.WriteLine("Load exisiting tracked times...");
                Console.WriteLine();
                foreach (var item in cmd.GetObjects().OrderBy(p => p.Begin))
                    Console.WriteLine(cmd.GetRepresentation(item));
            }

            if (target.ToLower().Equals("document"))
            {
                Console.WriteLine();
                Console.WriteLine("Adding new documents to store...");
                var count = XmlStoreUtils.UpdateTypeMapForDocuments();
                Console.WriteLine();
                Console.WriteLine(string.Format("{0} documents added!", count));
                Console.WriteLine();

                var cmd = new DocumentCommand();
                Console.WriteLine();
                Console.WriteLine("Load exisiting documents...");

                foreach (var item in cmd.GetObjects().OrderBy(p => p.Subject))
                    Console.WriteLine(cmd.GetRepresentation(item));

            }
        }
    }
}