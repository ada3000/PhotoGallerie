using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGalerie
{
    public class FolderInfo
    {
        public string Path;
        public List<Item> Folders = new List<Item>();

        public class Item
        {
            public string Title;
            public string Id;
        }
    }
}