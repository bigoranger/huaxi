using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace huaxi.Areas.Admin.Models
{
    public class DocumentModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "摘要")]
        public string Description { get; set; }

        [Required]
        public int CateID { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UserName { get; set; }

    }
    public class ArticleModel
    {

        [Required]
        [Display(Name = "内容")]
        public string Contents { get; set; }

        [Display(Name = "来源")]
        public string BeFrom { get; set; }

        public DocumentModel Document { get; set; }

    }

    public static class ArticleBLL
    {
        public static bool Insert(ArticleModel article, out string msg)
        {
            msg = "";
            Documents model = new Documents();
            Document_Article artModel = new Document_Article();
            model.CateID = article.Document.CateID;
            model.Title = article.Document.Title;
            model.Description = article.Document.Description;
            model.UserName = article.Document.UserName;
            model.CreateTime = DateTime.Now;
            model.UpdateTime = model.CreateTime;

            artModel.DocID = DocumentsDAL.Insert(model);
            if (artModel.DocID > 0)
            {
                artModel.Contents = article.Contents;
                artModel.BeFrom = article.BeFrom;
                Document_ArticleDAL.Insert(artModel);
                return true;
            }

            msg = "网络异常，请重试";
            return false;
        }


        internal static bool Delete(int id, int status)
        {
            Documents model = DocumentsDAL.GetById(id);
            model.Status = status;
            return DocumentsDAL.Update(model);
        }

        internal static ArticleModel GetById(int id)
        {
            Documents model = DocumentsDAL.GetById(id);
            Document_Article artmodel = Document_ArticleDAL.GetById(id);
            ArticleModel article = new ArticleModel();
            article.Document = new DocumentModel();

            article.BeFrom = artmodel.BeFrom;
            article.Contents = artmodel.Contents;
            article.Document.ID = model.ID;
            article.Document.Description = model.Description;
            article.Document.Title = model.Title;
            article.Document.CateID = model.CateID;
            return article;
        }

        internal static bool Update(ArticleModel article, out string msg)
        {
            msg = "";
            Documents model = DocumentsDAL.GetById(article.Document.ID);
            Document_Article artModel = Document_ArticleDAL.GetById(article.Document.ID);

            model.Title = article.Document.Title;
            model.Description = article.Document.Description;
            model.UpdateTime = DateTime.Now;
            model.Status = 0;
            artModel.Contents = article.Contents;
            artModel.BeFrom = article.BeFrom;
            artModel.DocID = article.Document.ID;
            if (DocumentsDAL.Update(model) && Document_ArticleDAL.Update(artModel))
                return true;

            msg = "网络异常，请重试";
            return false;
        }

        internal static void Destroy(int id)
        {
            DocumentsDAL.DeleteById(id);
            Document_ArticleDAL.DeleteById(id);
        }
    }
}