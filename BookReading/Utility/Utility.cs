using BookReading.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BookReading.Utility
{
    public static class Utility
    {
        public static void RemoveFile(string filePath, FileType fileType)
        {
            var fileFullPath = "";
            switch (fileType)
            {
                case FileType.Image:
                    fileFullPath = HttpContext.Current.Server.MapPath("~/Public/images/" + filePath);
                    break;
                case FileType.PDF:
                    fileFullPath = HttpContext.Current.Server.MapPath("~/Public/pdf/" + filePath);
                    break;
                default:
                    break;
            }

            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
            }
        }
    }
}