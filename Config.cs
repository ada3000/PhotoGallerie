using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGalerie
{
    public static class Config
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
                return _photoExtensions ?? (_photoExtensions = ExtractArr("photoExtensions"));
            }
        }

        private static string[] _videoExtensions;
        public static string[] VideoExtensions
        {
            get
            {
                return _videoExtensions ?? (_videoExtensions = ExtractArr("videoExtensions"));
            }
        }

        private static string[] _dataExtensions;
        public static string[] DataExtensions
        {
            get
            {
                return _dataExtensions ?? (_dataExtensions = ExtractArr("dataExtensions"));
            }
        }

        private static string[] ExtractArr(string appSettingsKey, string separator = ",")
        {
            return ConfigurationManager.AppSettings[appSettingsKey].ToLower().Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string _version = null;
        public static string Version
        {
            get
            {
                return _version ?? (_version = typeof(Config).Assembly.GetName().Version.ToString());
            }
        }
    }
}
