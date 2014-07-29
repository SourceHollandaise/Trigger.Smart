using System.Collections.Generic;
using System;

namespace Trigger.XForms.Controllers
{
    public static class ActionControllerDeclarator
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
}
