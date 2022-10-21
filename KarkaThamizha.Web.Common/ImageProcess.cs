using KarkaThamizha.Common.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace KarkaThamizha.Web.Common
{
    [CustomException]
    public class ImageProcess : Controller
    {
        public static string ImageUpload(HttpPostedFileBase imgFile, string category, string fileName = "")
        {
            try
            {
                if (imgFile != null && imgFile.ContentLength > 0)
                {
                    var validImageTypes = new string[]
                    {
                        "gif",
                        "jpeg",
                        "jpg",
                        "png"
                    };

                    var fileExt = System.IO.Path.GetExtension(imgFile.FileName.ToLower()).Substring(1);
                    if (validImageTypes.Contains(fileExt.ToLower()))
                    {
                        string imgPath = "";

                        if (string.IsNullOrEmpty(fileName))
                            fileName = Path.GetFileName(imgFile.FileName);

                        if (category == "Publisher")
                        {
                            imgPath = ConfigurationManager.AppSettings["ImgPublisher"];
                            imgFile.SaveAs(Path.Combine(imgPath + fileName));
                        }
                        else if (category == "Magazine")
                        {
                            imgPath = ConfigurationManager.AppSettings["ImgMagazine"];
                            imgFile.SaveAs(Path.Combine(imgPath + fileName));
                        }
                        else if (category == "Events")
                        {
                            imgPath = ConfigurationManager.AppSettings["ImgEvent"];
                            imgFile.SaveAs(Path.Combine(imgPath + fileName));
                        }
                        else if (category == "Articles")
                        {
                            imgPath = ConfigurationManager.AppSettings["ImgArticle"];
                            imgFile.SaveAs(Path.Combine(imgPath + fileName));
                        }
                    }
                    else { fileName = "Invalid type. Only the following types (jpg, jpeg, png) are supported."; }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fileName;
        }

        public static bool checkValidation(HttpPostedFileBase file)
        {
            ImageProcess process = new ImageProcess();
            bool flag = true;
            var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExt))
            {
                process.ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");
                flag = false;
            }
            return flag;
        }
    }
}