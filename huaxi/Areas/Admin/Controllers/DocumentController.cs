using huaxi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace huaxi.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public class DocumentController : Controller
    {
        //1.校内新闻
        //2.专卖推送
        //3.公告栏
        static int[] typeArr = { 1, 2, 3 };

        public ActionResult Index(int type = 1, int page = 1, string title = null, int status = 0)
        {
            List<string> ListWhere = new List<string>();
            string typeStr;
            string titleStr;
            string statusStr = "Status>-1";

            //查询删除的内容
            if (status == -1)
            {
                statusStr = "Status=-1";
                typeStr = "CateID in (" + string.Join(",", typeArr) + ")";
            }
            else
            {
                if (Array.IndexOf(typeArr, type) < 0)
                    type = typeArr[0];

                typeStr = "CateID=" + type;
            }

            ListWhere.Add(typeStr);
            ListWhere.Add(statusStr);

            if (title != null)
            {
                titleStr = string.Format("Title like '%{0}%'", title);
                ListWhere.Add(titleStr);
            }
            ViewBag.wd = title;
            string[] arrWhere = ListWhere.ToArray();
            IEnumerable<ViewArticle> List = ViewArticleDAL.ListByPage(page, 10, "UpdateTime", true, arrWhere);
            ViewBag.Count = ViewArticleDAL.ListCount(arrWhere);
            ViewBag.PageCount = ViewBag.Count / 10 + 1;
            ViewBag.StartNum = page * 10 - 9;
            ViewBag.EndNum = page * 10 < ViewBag.Count ? page * 10 : ViewBag.Count;
            ViewBag.Page = page;
            return View(List);

        }

        public ActionResult Create(int type = 1)
        {
            if (Array.IndexOf(typeArr, type) < 0)
                type = typeArr[0];
            try
            {
                ArticleModel model = new ArticleModel();
                model.Document = new DocumentModel();
                model.Document.CateID = type;
                model.Document.UserName = User.Identity.Name;
                return View(model);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //添加[新闻]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArticleModel model, int type = 1)
        {
            string msg = "";
            if (Array.IndexOf(typeArr, type) < 0)
                type = typeArr[0];
            if (ModelState.IsValid && ArticleBLL.Insert(model, out msg))
            {
                return RedirectToAction("Index", new { type = type });
            }
            else
            {
                ModelState.AddModelError("", msg);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ArticleModel model = ArticleBLL.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ArticleModel model)
        {
            string msg = "";
            if (ModelState.IsValid && ArticleBLL.Update(model, out msg))
            {
                return RedirectToAction("Index", new { type = model.Document.CateID });
            }
            else
            {
                ModelState.AddModelError("", msg);
                return View();
            }
        }

        public ActionResult Delete(string[] id, int status = -1, int type = 1)
        {
            if (Array.IndexOf(typeArr, type) < 0)
                type = typeArr[0];
            if (id != null && id.Length > 0)
            {
                foreach (string i in id)
                {
                    ArticleBLL.Delete(Convert.ToInt32(i), status);
                }

            }

            return RedirectToAction("Index", new { type = type });

        }

        //彻底删除
        public ActionResult Destroy(string[] id)
        {
            if (id != null && id.Length > 0)
            {
                foreach (string i in id)
                {
                    ArticleBLL.Destroy(Convert.ToInt32(i));
                }
            }
            return RedirectToAction("Index", new { status = -1 });
        }
    }
}
