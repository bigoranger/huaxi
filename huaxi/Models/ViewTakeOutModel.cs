using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;


namespace huaxi.Models
{
    public class ViewTakeOutModel
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int Comment { get; set; }

        public int Views { get; set; }

        public int CoverID { get; set; }

        public int Status { get; set; }

        public int DocID { get; set; }

        public string Contents { get; set; }

        public string Display { get; set; }

        public int PictureID { get; set; }

        public decimal Price { get; set; }

        public int RestuarantID { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Url { get; set; }

        public string Address { get; set; }
    }

    public static class ViewTakeOutBLL
    {
        private static ViewTakeOutModel ToModel(DataRow row)
        {
            ViewTakeOutModel model = new ViewTakeOutModel();
            model.ID = (System.Int32)row["ID"];
            model.UserName = (System.String)row["UserName"];
            model.Title = (System.String)row["Title"];
            model.Description = (System.String)row["Description"];
            model.Comment = (System.Int32)row["Comment"];
            model.Views = (System.Int32)row["Views"];
            model.Display = (System.String)row["Display"];
            model.Price = (System.Decimal)row["Price"];
            model.Contents = (System.String)row["Contents"];
            model.Name = (System.String)row["Name"];
            model.Telephone = (System.String)row["Telephone"];
            model.Url = (System.String)row["Url"];
            model.Address = (System.String)row["Address"];
            return model;
        }

        /// <summary>
        /// 获得所有外卖信息
        /// </summary>
        /// <returns>Document_TakeOut类的对象的枚举</returns>
        public static IEnumerable<ViewTakeOutModel> ListAll()
        {
            List<ViewTakeOutModel> list = new List<ViewTakeOutModel>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM [ViewTakeOut]");
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页数（从1开始计数）</param>
        /// <param name="num">每页个数（从1开始计数）</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="isDesc">是否降序</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static IEnumerable<ViewTakeOutModel> ListByPage(int page = 1, int num = 10, string orderBy = "UpdateTime", bool isDesc = true, params string[] where)
        {
            string whereStr = "";
            if (num < 1 || page < 1) { return null; }
            if (where != null && where.Length > 0) { whereStr = " and b." + string.Join(" and b.", where); }
            if (isDesc) { orderBy += " desc"; }

            List<ViewTakeOutModel> list = new List<ViewTakeOutModel>();
            DataTable dt = SqlHelper.ExecuteDataTable(string.Format(@"SELECT b.* FROM (SELECT a.*, ROW_NUMBER () OVER (ORDER BY a.{0}) AS RowNumber FROM ViewTakeOut AS a  ) AS b 
                           WHERE (1 = 1) {1} AND RowNumber BETWEEN {2} AND {3} ORDER BY b.RowNumber", orderBy, whereStr, page * num - num + 1, page * num));

            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }

        /// <summary>
        /// 查询总数量
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static int ListCount(params string[] where)
        {
            string whereStr = "";
            if (where != null && where.Length > 0)
            {
                whereStr = " and " + string.Join(" and ", where);
            }
            DataTable dt = SqlHelper.ExecuteDataTable(@"SELECT count(*) FROM ViewTakeOut where(1=1) " + whereStr);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}