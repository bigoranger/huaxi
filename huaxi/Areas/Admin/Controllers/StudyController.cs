using huaxi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace huaxi.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
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
        /// <summary>
        /// 分类管理
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult CateManage(int status = 0)
        {
            GetCateList();
            GetCateList(status, 1);
            ViewBag.Cate = CategoryModel.ListCateByID(8).ToList();
            return View();
        }
        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <param name="status"></param>
        /// <param name="type"></param>
        private void GetCateList(int status = 0, int type = 0)
        {
            if (type == 1)
            {
                ViewBag.ListGuideCate = CategoryModel.ListCateByID(typeArr[0], status).ToList();
                ViewBag.ListShareCate = CategoryModel.ListCateByID(typeArr[1], status).ToList();
                ViewBag.ListTrainningCate = CategoryModel.ListCateByID(typeArr[2], status).ToList();
                return;
            }
            ViewBag.GuideCate = CategoryModel.ListCateByID(typeArr[0], status).ToList();
            ViewBag.ShareCate = CategoryModel.ListCateByID(typeArr[1], status).ToList();
            ViewBag.TrainningCate = CategoryModel.ListCateByID(typeArr[2], status).ToList();
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
        /// <summary>
        /// 增加或修改分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modif"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCate(Category model, string modif = "add")
        {
            modif = modif.Trim();
            string msg = "";
            model.Status = 0;
            int st = 0;
            if (modif == "add") { st = CategoryBLL.Insert(model, ref msg) ? 1 : 0; }
            else if (modif == "update")
            {
                var ls = (List<Category>)CategoryDAL.ListByWhere(model, null, "Name");
                ls = ls.Where(a => a.ID != model.ID).ToList();
                if (ls.Count >= 1)
                {
                    return Json(new { status = 0, info = "标识已存在" });
                }
                st = CategoryDAL.Update(model) ? 1 : 0;
                if (st == 0) { msg = "保存失败"; }
            }
            else
            {
                st = 0; msg = "缺少操作参数";
            }
            return Json(new { status = st, info = msg });
        }
        /// <summary>
        /// 删除或回复分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult DeleteCate(string[] id, int status = -1)
        {
            if (id != null && id.Length > 0)
            {
                foreach (string i in id)
                {
                    CategoryBLL.Delete(Convert.ToInt32(i), status);
                }
            }
            return RedirectToAction("CateManage");
        }
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DestroyCate(string[] id)
        {
            if (id != null && id.Length > 0)
            {
                foreach (string i in id)
                {
                    CategoryBLL.Destroy(Convert.ToInt32(i));
                }
            }
            return RedirectToAction("CateManage", new { status = -1 });
        }
        /// <summary>
        /// ajax 获取分类数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCateById(int id)
        {
            return Json(CategoryDAL.GetById(id));
        }
    }
}
