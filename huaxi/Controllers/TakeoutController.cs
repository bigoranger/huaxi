using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using huaxi.Models;

namespace huaxi.Controllers
{
    public class TakeoutController : Controller
    {
        //
        // GET: /Takeout/

        /**
         * 获取外卖信息
         * author：DLG
         */
        static int[] typeArr = { 1, 2, 3 };
        public ActionResult Index(int type = 1, int page = 1, string title = null, int status = 0)
        {
            List<string> ListWhere = new List<string>();
            string titleStr;

            if (title != null)
            {
                titleStr = string.Format("Title like '%{0}%'", title);
                ListWhere.Add(titleStr);
            }

            string[] arrWhere = ListWhere.ToArray();
            IEnumerable<ViewTakeOutModel> List = ViewTakeOutBLL.ListByPage(page, 10, "UpdateTime", true, arrWhere);
            ViewBag.Count = ViewTakeOutBLL.ListCount(arrWhere);
            ViewBag.PageCount = ViewBag.Count / 10 + 1;
            ViewBag.StartNum = page * 10 - 9;
            ViewBag.EndNum = page * 10 < ViewBag.Count ? page * 10 : ViewBag.Count;
            ViewBag.Page = page;
            return View(List);
        }
    }
}