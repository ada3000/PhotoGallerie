using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGalerie
{
    static class FolderHelper
    {
        public static string GetFolderPath(string baseFolder, string folderParam)
        {
            var pageFolder = baseFolder;
            //Response.Write(pageFolder);
            if (folderParam != "")
            {
                string[] folderParams = folderParam.Split(',');
                foreach (var fp in folderParams)
                {
                    string[] dfr = Directory.GetDirectories(pageFolder);
                    List<string> df = new List<string>(dfr);
                    df.Sort();

                    pageFolder = df[int.Parse(fp)];
                }
            }

            return pageFolder;
        }
    }
}
