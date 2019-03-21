using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookReading.Extensions
{
    public enum FileType
    {
        Image,
        PDF
    }

    public static class FileExtension
    {
        public static bool IsImage(this HttpPostedFileBase file)
        {
            return file.ContentType == "image/png" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/gif";
        }

        public static bool IsPDF(this HttpPostedFileBase file)
        {
            return file.ContentType == "application/pdf";
        }



        public static string Save(this HttpPostedFileBase file, string subfolder, FileType fileType)
        {
            var fileName = Path.Combine(subfolder, Guid.NewGuid().ToString() + Path.GetFileName(file.FileName));

            var fileFullname = "";
            switch (fileType)
            {
                case FileType.Image:
                    fileFullname = HttpContext.Current.Server.MapPath("~/Public/images/" + fileName);
                    break;
                case FileType.PDF:
                    fileFullname = HttpContext.Current.Server.MapPath("~/Public/pdf/" + fileName);
                    break;
                default:
                    break;
            }

            file.SaveAs(fileFullname);

            return fileName;
        }
    }
}
