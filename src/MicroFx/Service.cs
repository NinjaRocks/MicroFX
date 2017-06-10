using System.Reflection;

namespace MicroFx
{
    public class Service
    {
        public static string GetName()
        {
            var asseblyName = Assembly.GetEntryAssembly().GetName();
            return asseblyName.Name;
        }
    }
}