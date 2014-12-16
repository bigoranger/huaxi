using System.Collections.Generic;

namespace huaxi.Areas.Admin.Models
{
    public class CategoryModel
    {
        public static IEnumerable<Category> ListCateByID(int type,int status=0)
        {
            Category model = new Category();
            model.PID = type;
            model.Status = status;
            return CategoryDAL.ListByWhere(model, null, "PID", "Status");
        }
    }
    public static class CategoryBLL {

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Insert(Category model, ref string msg)
        {
           var ls = (List<Category>)CategoryDAL.ListByWhere(model, null, "Name");
           if (ls.Count >= 1) {
               msg = "标识已存在";
               return false;
           }
            return CategoryDAL.Insert(model);
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool Delete(int id, int status)
        {
            var model = CategoryDAL.GetById(id);
            model.Status = status;
            return CategoryDAL.Update(model);
        }
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Destroy(int id)
        {
            return CategoryDAL.DeleteById(id);
        }
    }
}