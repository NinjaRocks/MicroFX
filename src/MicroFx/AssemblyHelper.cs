using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MicroFx
{
    public static class AssemblyHelper
    {
        public static List<T> GetTypes<T>(Assembly assembly)
        {
            var modules = assembly.GetTypes()
                .Where(t => typeof (T).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t=> (T)Activator.CreateInstance(t))
                .ToList();

            return modules;
        }

        public static List<T> Scan<T>(bool includeReferencedAssemblies = false)
        {
            var types = new List<T>();
           
            types.AddRange(GetTypes<T>(Assembly.GetEntryAssembly()));

            if (!includeReferencedAssemblies)
                return types;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (s, e) =>
            {
                var a = Assembly.ReflectionOnlyLoad(e.Name);
                if (a == null) throw new TypeLoadException("Could not load assembly " + e.Name);
                return a;
            };

            foreach (var assemblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                try
                {
                    var assembly = Assembly.ReflectionOnlyLoad(assemblyName.FullName);
                    types.AddRange(GetTypes<T>(assembly));

                }
                catch (FileNotFoundException)
                {
                   
                }
            }

            return types;
        }

    }
}