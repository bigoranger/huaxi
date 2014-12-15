using huaxi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace huaxi.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //验证码
        static string yzmStr = "";

        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, bool isRemember = false)
        {
            //判断用户是否存在
            if (ModelState.IsValid)
            {
                Users users = UsersBLL.Login(model);
                if (users != null)
                {
                    //将信息存入cookies内（最后一个属性应该存的是权限）
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        users.Name,
                        DateTime.Now,
                        DateTime.Now.AddDays(1),
                        isRemember,
                        users.RoleID.ToString(),
                        "/");
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    if (Request["ReturnUrl"] != null)
                        return Redirect(Request["ReturnUrl"]);
                    return Redirect("/");
                }
            }

            ModelState.AddModelError("", "提供的用户名或密码不正确。");
            return View();
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult YZM()
        {
            Helper.YZMHelper yzm = new Helper.YZMHelper();
            yzmStr = yzm.Text;
            MemoryStream ms = new MemoryStream();
            yzm.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return File(ms.ToArray(), "image/jpeg");
        }

        [HttpPost]
        public ActionResult Create(RegisterModel model)
        {
            string msg = "内容未填写完整";
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", msg);
                return View();
            }
            if (!model.YZM.Equals(yzmStr))
            {
                ModelState.AddModelError("", "验证码错误");
                return View();
            }
            if (UsersBLL.Insert(model, out msg))
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError("", msg);
                return View();
            }
        }
    }
}
