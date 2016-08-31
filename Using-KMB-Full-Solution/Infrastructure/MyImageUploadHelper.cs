using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Using_KMB_Full_Solution.Infrastructure
{
    public partial class MyImageUploadHelper
    {
        public static string SaveImage(HttpPostedFileBase image)
        {
            if (image == null)
            {
                throw new Exception(HttpContext.Current.Request.UrlReferrer.ToString() + " adresinde resim dosyası boş geçilmiştir.");
            }

            int counter = 0;
            string filePath = ConfigurationManager.AppSettings["UploadImagePathName"] + @"/" + image.FileName;

            while (File.Exists(HttpContext.Current.Server.MapPath(filePath)))
            {
                filePath = ConfigurationManager.AppSettings["UploadImagePathName"] + @"/" + counter.ToString() + "_" + image.FileName;
                counter++;
            }

            image.SaveAs(HttpContext.Current.Server.MapPath(filePath));

            return filePath;
        }
    }
}
