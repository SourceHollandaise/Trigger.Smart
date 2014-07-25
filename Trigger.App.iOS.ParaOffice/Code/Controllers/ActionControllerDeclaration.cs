using System.Collections.Generic;
using Trigger.XForms.Controllers;
using System.Linq;
using System;

namespace Trigger.XForms.Controllers
{
	public static class ActionControllerDeclaration
	{
		public static IList<Type> DeclaredControllerTypes = new List<Type>();

		public static void DeclareControllerTypes(IEnumerable<Type> controllerTypes)
		{
			foreach (var controller in controllerTypes)
			{
				DeclaredControllerTypes.Add(controller);
			}
		}
	}

	public static class ModelTypesDeclaration
	{
		public static IList<Type> DeclaredModelTypes = new List<Type>();

		public static void DeclareModelTypes(IEnumerable<Type> modelTypes)
		{
			foreach (var controller in modelTypes)
			{
				DeclaredModelTypes.Add(controller);
			}
		}
	}
}
