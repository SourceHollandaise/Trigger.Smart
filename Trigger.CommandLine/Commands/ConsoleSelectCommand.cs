
using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.Datastore.Security;

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
				Console.WriteLine("Load issues...");
				Console.WriteLine();
               
				foreach (var item in Store.LoadAll<IssueTracker>().OrderBy( p => p.Created))
					Console.WriteLine(item.GetRepresentation());
			}

			if (target.ToLower().Equals("project"))
			{
				Console.WriteLine("Load projects...");
				Console.WriteLine();

				foreach (var item in Store.LoadAll<Project>().OrderBy( p => p.Name))
				{
					Console.WriteLine(item.GetRepresentation());
					Console.WriteLine("Linked documents:");
					foreach (var doc in item.LinkedDocuments)
					{
						Console.WriteLine(doc.GetRepresentation());
					}
					Console.WriteLine("Linked issues:");
					foreach (var issue in item.LinkedIssues)
					{
						Console.WriteLine(issue.GetRepresentation());
					}
					Console.WriteLine("Linked tracked times:");
					foreach (var time in item.LinkedTrackedTimes)
					{
						Console.WriteLine(time.GetRepresentation());
					}
					Console.WriteLine("================");
					Console.WriteLine();
				}
			}

			if (target.ToLower().Equals("times"))
			{
				Console.WriteLine("Load tracked times...");
				Console.WriteLine();
              
				foreach (var item in Store.LoadAll<TimeTracker>().OrderBy( p => p.Begin))
					Console.WriteLine(item.GetRepresentation());
			}

			if (target.ToLower().Equals("document"))
			{
//				Console.WriteLine();
//				Console.WriteLine("Add new documents to store...");
//				var count = new Trigger.CRM.Services.FileUpdateService().LoadFromDocumentStore();
//				Console.WriteLine();
//				Console.WriteLine(string.Format("{0} documents added!", count));
//				Console.WriteLine();
//
//				Console.WriteLine();
//				Console.WriteLine("Load documents...");

				foreach (var item in Store.LoadAll<Document>().OrderBy( p => p.Created).ThenBy(p => p.Subject))
					Console.WriteLine(item.GetRepresentation());
			}
		}
	}
}