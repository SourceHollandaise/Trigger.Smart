
using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.CRM.Services;
using Trigger.Datastore.Security;

namespace Trigger.CommandLine.Commands
{
	public class ConsoleInsertUpdateCommand : ConsoleBaseCommand
	{
		public static void InsertUpdateItems(string target)
		{
			if (target.ToLower().Equals("user"))
			{
				InsertUpdateUser();
			}

			if (target.ToLower().Equals("issue"))
			{
				InsertUpdateIssue();
			}

			if (target.ToLower().Equals("project"))
			{
				InsertUpdateProject();
			}

			if (target.ToLower().Equals("times"))
			{
				//var cmd = new TimeTrackerCommand();
			}

			if (target.ToLower().Equals("document"))
			{
				InsertUpdateDocument();
			}
		}

		static User InsertUpdateUser()
		{
           
			Console.WriteLine("Username:");
			var userName = Console.ReadLine();
			var user = Store.LoadAll<User>().FirstOrDefault(p => p.UserName == userName);

			if (user == null)
			{
				Console.WriteLine("User doesn't exists. Add new user informations.");

				Console.WriteLine("E-Mail:");
				var email = Console.ReadLine();
				Console.WriteLine("Password:");
				var password = Console.ReadLine();
				Console.WriteLine("Retype password:");
				var passwordToCompare = Console.ReadLine();

				if (!password.Equals(passwordToCompare))
				{
					Console.WriteLine("Passwords are not equal!");
					return null;
				}

				user = new User();
				user.Initialize();
				user.UserName = userName;
				user.SetPassword(password);

				if (!string.IsNullOrWhiteSpace(email))
					user.EMail = email;

				user.Save();
				return user;

			}
			else
			{
				Console.WriteLine("User already exists. Update user informations.");

				var currentPassword = user.Password;

				Console.WriteLine("Current password:");
				var password = Console.ReadLine();

				if (!SecureText.Compare(password, currentPassword))
				{
					Console.WriteLine("Password is not valid!");
					return user;
				}

				Console.WriteLine("Update password? Press <Enter> to update or any key to continue!");

				if (Console.ReadKey().Key == ConsoleKey.Enter)
				{
					Console.WriteLine("Password:");
					var newPassword = Console.ReadLine();
					Console.WriteLine("Retype password:");
					var passwordToCompare = Console.ReadLine();

					if (!newPassword.Equals(passwordToCompare))
					{
						Console.WriteLine("Passwords are not equal!");
						return null;
					}
					else
						user.SetPassword(newPassword);
				}

				Console.WriteLine("Update E-Mail? Press <Enter> to update or any key to continue!");

				if (Console.ReadKey().Key == ConsoleKey.Enter)
				{
					Console.WriteLine("E-Mail:");
					var email = Console.ReadLine();
					if (!string.IsNullOrWhiteSpace(email))
						user.EMail = email;
				}

				user.Save();

				return user;
			}
		}

		static Project InsertUpdateProject()
		{
           
			Console.WriteLine("Projectname:");
			var projectName = Console.ReadLine();
			Console.WriteLine("Add some informations:");
			var description = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(projectName))
				return null;

			var project = Store.LoadAll<Project>().FirstOrDefault(p => p.Name == projectName);
			if (project == null)
			{
				project = new Project();
				project.Initialize();
				project.Name = projectName;
			}

			if (!string.IsNullOrWhiteSpace(description))
				project.Description = description;

			project.Save();

			return project;
		}

		static Document InsertUpdateDocument()
		{

			Console.WriteLine("Add Subject for document:");
			var subject = Console.ReadLine();
			Console.WriteLine("Filename:");
			var fileName = Console.ReadLine();

          
			var document = Store.LoadAll<Document>().FirstOrDefault(p => p.Subject == subject && p.FileName == fileName);

          
			if (document == null)
			{
				document = new Document();
				document.Initialize();
				document.Subject = subject;
				document.User = CurrentUser;
				new DocumentService(document).AddFile(fileName);
			}
			else
			{
				Console.WriteLine("Document exists! Overwrite? Press <Enter> for override or any key to continue!");
				if (Console.ReadKey().Key == ConsoleKey.Enter)
					new DocumentService(document).AddFile(fileName);
			}

			document.Project = InsertUpdateProject();

			document.Save();

			return document;
		}

		static IssueTracker InsertUpdateIssue()
		{
			Console.WriteLine("Set Subject: ");

			var subject = Console.ReadLine();

           
			var issue = Store.LoadAll<IssueTracker>().FirstOrDefault(p => p.Subject == subject);

			if (issue == null)
			{
				Console.WriteLine("Issue does not existing! Press <Enter> to create new or any other key to continue!");
				if (Console.ReadKey().Key == ConsoleKey.Enter)
				{
					issue = new IssueTracker
					{
						Subject = subject,
						Issue = IssueType.Incident,
						State = IssueState.Open
					};
					issue.Initialize();
				}
				else
					return null;
			}
			Console.WriteLine("Set Description: ");
			var description = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(description))
				issue.Description = description;
			Console.WriteLine("Set Issue: Bug = 1, Incident = 2, Request = 11, ChangeRequest = 12, EnhancementRequest = 13, Information = 21");
			var type = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(type))
				issue.Issue = (IssueType)Convert.ToInt32(type);
			Console.WriteLine("Set State: Open = 1, Accepted = 2, InProgress = 3, Done = 4, Rejected = 10");
			var state = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(state))
				issue.State = (IssueState)Convert.ToInt32(state);
			var project = InsertUpdateProject();
			if (project != null)
				issue.Project = project;
			issue.Save();

			return issue;
		}
	}
}