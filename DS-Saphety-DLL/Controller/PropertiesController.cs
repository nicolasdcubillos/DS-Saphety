using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS_Saphety_DLL.Controller
{
    internal class PropertiesController
    {
        private static string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        private static string iconPath = Path.Combine(outPutDirectory, "Utils/settingsDS.ini");
        private static string path = new Uri(iconPath).LocalPath; 
        private static IniFile file = new IniFile(path);
        public void write (String key, String value)
        {
            file.Write(key, value);
        }

        public String read(String key)
        {
            return file.Read(key);
        }
    }
}
