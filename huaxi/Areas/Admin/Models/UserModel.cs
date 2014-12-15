using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace huaxi.Areas.Admin.Models
{
    public class UserModel {
        public IEnumerable<System.Web.Mvc.SelectListItem> ListCate()
        {
            var list = UsersDAL.ListAll().Select(a => new System.Web.Mvc.SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.Name
            });
            return list;
        }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "账号")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "账号")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
      
        [Required]
        [Display(Name = "验证码")]
        public string YZM { get; set; }
    }

    public static class UsersBLL
    {
        //检测是否重名
        public static bool isFound(string username)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * from Users WHERE Name=@Name", new SqlParameter("@Name", username));
            return dt.Rows.Count > 0;
        }

        //通过用户名和密码获取用户信息
        public static Users Login(LoginModel users)
        {
            users.Password = MD5Helper.GetMD5(users.Password);
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * from Users WHERE Name=@Name and Password=@Password", new SqlParameter("@Name", users.Name), new SqlParameter("@Password", users.Password));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            return UsersDAL.ToModel(row);
        }

        //添加新用户
        public static bool Insert(RegisterModel users, out string msg)
        {
            if (isFound(users.UserName))
            {
                msg = "用户名重复";
                return false;
            }

            users.Password = Helper.MD5Helper.GetMD5(users.Password);
            Users model = new Users();
            model.Password = users.Password;
            model.Name = users.UserName;

            msg = "网络异常，请重试";
            return UsersDAL.Insert(model);
        }

    }

}