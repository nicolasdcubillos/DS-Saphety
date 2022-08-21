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
        private static string principalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        private static string settingsPath = Path.Combine(principalPath, "Utils/settingsDS.ini");
        private static string path = new Uri(settingsPath).LocalPath; 
        private static IniFile file = new IniFile(path);
        public PropertiesController ()
        {
            try {
                principalPath = this.read("path");
            } catch {
                principalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\DocumentosSoporte\\";
                this.write("path", principalPath);
            }
        }
        public void write (String key, String value) { file.Write(key, value); }
        public String read(String key) { return file.Read(key); }
    }
}