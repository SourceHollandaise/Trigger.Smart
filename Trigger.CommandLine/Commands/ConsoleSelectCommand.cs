
using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.CRM.Persistent;

namespace Trigger.CommandLine.Commands
{
    public class ConsoleSelectCommand : ConsoleBaseCommand
    {
        public static void ListItems(string target)
        {
            if (target.ToLower().Equals("user"))
            {


                Console.WriteLine("Load exisiting users...");
                Console.WriteLine();
                foreach (var item in Store.LoadAll<User>().OrderBy( p => p.UserName))
                    Console.WriteLine(item.GetRepresentation());

            }

            if (target.ToLower().Equals("issue"))
            {
                Console.WriteLine("Load exisiting issues...");
                Console.WriteLine();
               
                foreach (var item in Store.LoadAll<IssueTracker>().OrderBy( p => p.Created))
                    Console.WriteLine(item.GetRepresentation());

            }

            if (target.ToLower().Equals("project"))
            {
                Console.WriteLine("Load exisiting projects...");
                Console.WriteLine();

                foreach (var item in Store.LoadAll<Project>().OrderBy( p => p.Name))
                    Console.WriteLine(item.GetRepresentation());
            }

            if (target.ToLower().Equals("times"))
            {
                Console.WriteLine("Load exisiting tracked times...");
                Console.WriteLine();
              
                foreach (var item in Store.LoadAll<TimeTracker>().OrderBy( p => p.Begin))
                    Console.WriteLine(item.GetRepresentation());
            }

            if (target.ToLower().Equals("document"))
            {
                Console.WriteLine();
                Console.WriteLine("Adding new documents to store...");
                var count = StoreUtils.UpdateTypeMapForDocuments();
                Console.WriteLine();
                Console.WriteLine(string.Format("{0} documents added!", count));
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Load exisiting documents...");

                foreach (var item in Store.LoadAll<Document>().OrderBy( p => p.Created).ThenBy(p => p.Subject))
                    Console.WriteLine(item.GetRepresentation());

            }
        }
    }
}