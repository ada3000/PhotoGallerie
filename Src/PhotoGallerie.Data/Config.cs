using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallerie.Data
{
    internal static class Config
    {
        public static string DbFilePath
        {
            get { return ConfigurationManager.AppSettings["DbFilePath"]; }
        }
    }
}
