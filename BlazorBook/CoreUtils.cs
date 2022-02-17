using System.IO;
using System.Reflection;

namespace BlazorBook
{
    public static class CoreUtils
    {
        public static string GetEmbeddedWebResource(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"BlazorBook.wwwroot.{name}"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
