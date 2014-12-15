using Helper;
using System;
using System.Collections.Generic;
using System.Data;

namespace huaxi
{
    public static class ViewArticleDAL
    {
        public static ViewArticle ToModel(DataRow row)
        {
            ViewArticle model = new ViewArticle();
            model.ID = (System.Int32)row["ID"];
            model.Contents = (System.String)row["Contents"];
            model.BeFrom = (System.String)SqlHelper.FromDBValue(row["BeFrom"]);
            model.Title = (System.String)row["Title"];
            model.Description = (System.String)SqlHelper.FromDBValue(row["Description"]);
            model.UserName = (System.String)row["UserName"];
            model.DocUrl = (System.String)row["DocUrl"];
            model.UserUrl = (System.String)row["UserUrl"];
            model.Status = (System.Int32)row["Status"];
            model.StatusName = (System.String)row["StatusName"];
            model.Cate = (System.String)row["Display"];
            model.CateID = (System.Int32)row["CateID"];
            model.CreateTime = (System.DateTime)SqlHelper.FromDBValue(row["CreateTime"]);
            model.UpdateTime = (System.DateTime?)SqlHelper.FromDBValue(row["UpdateTime"]);
            model.Comment = (System.Int32)row["Comment"];
            model.Views = (System.Int32)row["Views"];
            return model;
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
        public static IEnumerable<ViewArticle> ListByPage(int page = 1, int num = 10, string orderBy = "UpdateTime", bool isDesc = true, params string[] where)
        {
            string whereStr = "";
            if (num < 1 || page < 1) { return null; }
            if (where != null && where.Length > 0) { whereStr = " and b." + string.Join(" and b.", where); }
            if (isDesc) { orderBy += " desc"; }

            List<ViewArticle> list = new List<ViewArticle>();
            DataTable dt = SqlHelper.ExecuteDataTable(string.Format(@"SELECT
	                                                        b.*
                                                        FROM
	                                                        (
		                                                        SELECT
			                                                        a.*, ROW_NUMBER () OVER (ORDER BY a.{0}) AS RowNumber
		                                                        FROM
			                                                        ViewArticle AS a
	                                                        ) AS b
                                                        WHERE
	                                                        (1 = 1) {1}
                                                        AND RowNumber BETWEEN {2} AND {3}
                                                        ORDER BY
	                                                        b.RowNumber", orderBy
                                                                        , whereStr
                                                                        , page * num - num + 1
                                                                        , page * num));

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
            DataTable dt = SqlHelper.ExecuteDataTable(@"SELECT count(*) FROM ViewArticle where(1=1) " + whereStr);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
