using System.Collections.Generic;

namespace huaxi.Areas.Admin.Models
{
    public class CategoryModel
    {
        public static IEnumerable<Category> ListCateByID(int type)
        {
            Category model = new Category();
            model.PID = type;
            return CategoryDAL.ListByWhere(model, null, "PID");
        }
    }
}