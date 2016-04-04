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

        private static string[] _photoExtensions;
        public static string[] PhotoExtensions
        {
            get
            {
                return _photoExtensions ?? (_photoExtensions=System.Configuration.ConfigurationManager.AppSettings["photoExtensions"].Split(','));                
            }
        }

        private static string[] _videoExtensions;
        public static string[] VideoExtensions
        {
            get
            {
                return _videoExtensions ?? (_videoExtensions = System.Configuration.ConfigurationManager.AppSettings["videoExtensions"].Split(','));
            }
        }

        private static string[] _dataExtensions;
        public static string[] DataExtensions
        {
            get
            {
                return _dataExtensions ?? (_dataExtensions = System.Configuration.ConfigurationManager.AppSettings["dataExtensions"].Split(','));
            }
        }
    }
}
