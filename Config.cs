using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGalerie
{
    static class Config
    {
        public static string BaseFolder
        {
            get
            {
                FileInfo fi = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).Replace("/", "\\"));
                
                return Path.Combine(fi.Directory.FullName, System.Configuration.ConfigurationManager.AppSettings["folder"]);
            }
        }
    }
}
