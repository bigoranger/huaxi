using huaxi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huaxi.Areas.Admin.Controllers
{
    [Authorize(Roles="1")]
    public class StudyController : Controller
    {

        //7.就业指导
        //9.资源共享
        //10.培训信息
        int[] typeArr = { 7, 9, 10 };
        public ActionResult Index(int type = 7, int page = 1, string title = null, int status = 0)
        {
            GetCateList();
            List<string> ListWhere = new List<string>();
            string typeStr;
            string titleStr;
            string statusStr = "Status>-1";

            if (Array.IndexOf(typeArr, type) < 0)
                type = typeArr[0];

            typeStr = "CateID=" + type;
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
        public ActionResult CateManage() {
            GetCateList();
            ViewBag.Cate = CategoryModel.ListCateByID(8).ToList();
            return View();
        }
        private void GetCateList()
        {
            ViewBag.GuideCate = CategoryModel.ListCateByID(typeArr[0]).ToList();
            ViewBag.ShareCate = CategoryModel.ListCateByID(typeArr[1]).ToList();
            ViewBag.TrainningCate = CategoryModel.ListCateByID(typeArr[2]).ToList();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArticleModel model)
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ArticleModel model)
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCate(Category model,string modif="add") {

            string msg = "";
            model.Status = 0;
            int st=CategoryModel.Insert(model,ref msg)?1:0;
            return Json(new {status=st,info=msg });
        }
    }
}
