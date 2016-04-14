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
        public static FolderInfo GetFolderPath(string baseFolder, string folderParam)
        {
            FolderInfo result = new FolderInfo();

            var pageFolder = baseFolder;
            //Response.Write(pageFolder);
            if (folderParam != "")
            {
                string[] folderParams = folderParam.Split(',');
                string currentFolderId = "";
                string folderName = "";

                foreach (var folderIndex in folderParams)
                {
                    currentFolderId += currentFolderId == "" ? folderIndex : "," + folderIndex;

                    List<string> folders = Directory.GetDirectories(pageFolder).ToList();
                    folders.Sort();

                    pageFolder = folders[int.Parse(folderIndex)];
                    folderName = pageFolder.Substring(pageFolder.LastIndexOf("\\")+1);

                    result.Folders.Add(new FolderInfo.Item
                    {
                        Title = folderName,
                        Id = currentFolderId
                    });
                }
            }

            result.Path = pageFolder;

            return result;
        }
    }
}
