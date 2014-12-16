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
            return CategoryDAL.ListByWhere(model, null, "PID");
        }
        public static bool Insert(Category model,ref string msg) {
            return CategoryDAL.Insert(model);
        }
    }
}