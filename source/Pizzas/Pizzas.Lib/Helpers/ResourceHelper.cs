using System.IO;
using System.Linq;
using System.Reflection;

namespace Pizzas.Lib.Helpers
{
    public static class ResourceHelper
    {
        public static Stream GetManifestResourceStream(string fileName)
        {
            //TODO : Grab Assembly containing embedded files.
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .First(manifestResourceName => manifestResourceName.Contains(fileName));
            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
