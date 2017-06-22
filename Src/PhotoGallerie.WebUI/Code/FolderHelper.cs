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
                    int folderHash = int.Parse(folderIndex);

                    pageFolder = folders
                        .First(f => f.GetFolderId() == folderHash);

                    folderName = pageFolder.Substring(pageFolder.LastIndexOf("\\") + 1);

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

        public static int GetFolderId(this string folder)
        {
            return folder.Substring(Config.BaseFolder.Length + 1).GetHashCode();
        }

        public static List<FileDesc> GetFiles(string folder)
        {
            List<FileDesc> result = new List<FileDesc>();

            string[] displayImagesRaw = Directory.GetFiles(folder);

            List<string> displayImages = new List<string>(displayImagesRaw);
            displayImages.Sort();

            foreach (string filePath in displayImages)
            {
                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                string fileExt = fileName.Split('.').Last().ToLower();

                FileDesc fileDesc = new FileDesc { Path = filePath, Ext = fileExt, Name = fileName };

                if (Config.PhotoExtensions.Contains(fileExt))
                    fileDesc.Type = FileDescType.Photo;

                if (Config.VideoExtensions.Contains(fileExt))
                    fileDesc.Type = FileDescType.Video;

                if (Config.DataExtensions.Contains(fileExt))
                    fileDesc.Type = FileDescType.Data;

                if (fileDesc.Type != FileDescType.Unknown)
                    result.Add(fileDesc);
            }

            return result;
        }
    }
}
