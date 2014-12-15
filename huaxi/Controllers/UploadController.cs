using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace huaxi.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadFile(string dir)
        {
            new Hashtable();
            var savePath = Url.Content("~/Upload/Attached/");
            var saveUrl = savePath;
            const int maxSize = 1000000;
            var extTable = new Hashtable
                               {
                                   {"image", "gif,jpg,jpeg,png,bmp"},
                                   {"flash", "swf,flv"},
                                   {"media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb"},
                                   {"file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2"}
                               };
            //switch (dir)
            //{
            //    case "image":
            //        savePath = Url.Content("~/Upload/Images/");
            //        saveUrl = savePath;
            //        break;
            //    case "flash":
            //        break;
            //    case "media":
            //        break;
            //    case "file":
            //        break;
            //}
            HttpPostedFileBase file = Request.Files["imgFile"];
            var dirPath = Server.MapPath(savePath);
            var fileName = file.FileName;
            var fileExt = Path.GetExtension(fileName).ToLower();

            if (file == null)
                return UploadJsonRe(1, "请选择文件", "");

            if (!Directory.Exists(dirPath))
                return UploadJsonRe(1, "上传目录不存在", "");

            if (file.InputStream == null || file.InputStream.Length > maxSize)
                return UploadJsonRe(1, "上传文件大小超过限制", "");

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dir]).Split(','), fileExt.Substring(1).ToLower()) == -1)
                return UploadJsonRe(1, "上传文件扩展名是不允许的扩展名", "");

            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            var filePath = dirPath + newFileName;
            file.SaveAs(filePath);

            //压缩图片
            if (dir == "image")
                Helper.ImageClass.Compress(filePath);

            ////往数据库内插入数据
            //if (docid > 0)
            //    SaveInDB(docid, dir, file, filePath, newFileName);

            var fileUrl = saveUrl + newFileName;

            return UploadJsonRe(0, "", fileUrl);
        }

        private void SaveInDB(int docid, string dir, HttpPostedFileBase file, string filePath, string newFileName)
        {
            if (dir == "image")
            {
                Picture model = new Picture();
                model.Type = file.ContentType;
                model.MD5 = Helper.MD5Helper.GetMD5(file);
                model.Url = filePath;

                PictureDAL.Insert(model);
            }
            else
            {
                Files model = new Files();
                model.MD5 = Helper.MD5Helper.GetMD5(file);
                model.SavePath = filePath;
                model.SaveName = newFileName;
                model.Name = file.FileName;
                model.Size = file.ContentLength;
                model.Mime = file.ContentType;
                model.CreateTime = DateTime.Now;
                model.Ext = Path.GetExtension(model.Name).ToLower();

                FilesDAL.Insert(model);
            }

        }

        private JsonResult UploadJsonRe(int error, string message, string url)
        {
            var hash = new Hashtable();
            hash["error"] = error;
            hash["message"] = message;
            hash["url"] = url;
            return Json(hash, "text/html;charset=UTF-8");
        }
    }
}
