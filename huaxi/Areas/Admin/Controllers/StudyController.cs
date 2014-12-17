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
        public ActionResult Index(int type = 0, int page = 1, string title = null, int status = 0)
        {
            GetCateList();
            List<string> ListWhere = new List<string>();
            string typeStr;
            string titleStr;
            string statusStr = "Status>-1";
            if (status == -1)
            {
                statusStr = "Status=-1";
            }
            else {
                typeStr = "CateID=" + type;
                ListWhere.Add(typeStr);
            }
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
        /// <summary>
        /// 渲染添加学习视图
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Create(int type = 0)
        {
            GetCateList();
            if (type == 0)
            {
                List<Category> ls1 = (List<Category>)ViewBag.GuideCate;
                ls1.AddRange((List<Category>)ViewBag.ShareCate);
                ls1.AddRange((List<Category>)ViewBag.TrainningCate);
                if (ls1.Count > 0)
                {
                    type = ls1[0].ID;
                }
                else {
                    ArticleModel model = null;
                    return View(model);
                }
            }
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
        /// <summary>
       /// 添加[学习]
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArticleModel model, int type)
        {
            string msg = "";
            if (ModelState.IsValid && ArticleBLL.Insert(model, out msg))
            {
                return RedirectToAction("Index", new { type = type });
            }
            else
            {
                ModelState.AddModelError("", msg);
                GetCateList();
                return View();
            }
        }

        public ActionResult Delete(string[] id, int status = -1, int type=0)
        {
            if (id != null && id.Length > 0)
            {
                foreach (string i in id)
                {
                    ArticleBLL.Delete(Convert.ToInt32(i), status);
                }

            }
            return RedirectToAction("Index", new { type = type });
        }
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

        /// <summary>
        /// 渲染编辑文章view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            GetCateList();
            ArticleModel model = ArticleBLL.GetById(id);
            return View(model);
        }
        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                GetCateList();
                return View();
            }
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
