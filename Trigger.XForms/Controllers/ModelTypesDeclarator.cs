using System.Collections.Generic;
using System;

namespace Trigger.XForms.Controllers
{

    public static class ModelTypesDeclarator
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
