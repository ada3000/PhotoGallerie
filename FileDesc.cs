using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGalerie
{
    public class FileDesc
    {
        public string Path;
        public string Name;
        public string Ext;

        public FileDescType Type;
    }

    public enum FileDescType
    {
        Unknown = 0,
        Photo = 10,
        Video = 20,
        Data = 30
    }
}