using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMS.Utility
{
   public class ImageUpload
    {
       public static string ImageUploadFile(HttpPostedFileBase imageUpload, string dirFullPath, string dirPath)
       {
           //string dirPath = @"~/Images";
           //string dirFullPath = Server.MapPath(dirPath);
       
           string[] files;
           int numFiles;
           files = System.IO.Directory.GetFiles(dirFullPath);
           numFiles = files.Length;

           long maxId = 0;
           string file = null;
           string imageUrl = null;
           if (imageUpload != null && imageUpload.ContentLength > 0)
           {

               if (numFiles > 0)
                   maxId = numFiles + 1;
              // var uploadDir = "~/Images";
               string pdf = imageUpload.FileName;
               string ext = Path.GetExtension(pdf);
               //if (ext == ".jpg")
               //{
               file =+ maxId + "_" + pdf;
               var imagePath = Path.Combine(dirFullPath, file);
               imageUrl = Path.Combine(dirFullPath, file);
               imageUpload.SaveAs(imagePath);
               //}
           }
           return file != null ? dirPath + "/" + file : null;
       }
    }
}
