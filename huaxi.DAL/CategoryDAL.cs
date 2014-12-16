using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi
{
    public static class CategoryDAL
    {

        private static Category ToModel(DataRow row)
        {
            Category model = new Category();
            model.ID = (System.Int32)row["ID"];
            model.Name = (System.String)row["Name"];
            model.Display = (System.String)row["Display"];
            model.Status = (System.Int32)row["Status"];
            model.PID = (System.Int32)row["PID"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Category类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Category model)
        {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Category]( [Name], [Display], [Status], [PID]) VALUES( @Name, @Display, @Status, @PID)"
            , new SqlParameter("@Name", model.Name)
            , new SqlParameter("@Display", model.Display)
            , new SqlParameter("@Status", model.Status)
            , new SqlParameter("@PID", model.PID)
            );
            return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 ID)
        {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Category] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Category类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Category model)
        {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Category] SET [Name]=@Name, [Display]=@Display, [Status]=@Status, [PID]=@PID WHERE [ID]=@ID"
            , new SqlParameter("@ID", model.ID)
            , new SqlParameter("@Name", model.Name)
            , new SqlParameter("@Display", model.Display)
            , new SqlParameter("@Status", model.Status)
            , new SqlParameter("@PID", model.PID)
            );
            return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Category类的对象</returns>
        public static Category GetById(System.Int32 ID)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Display], [Status], [PID] FROM [Category] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            Category model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Category类的对象的枚举</returns>
        public static IEnumerable<Category> ListAll()
        {
            List<Category> list = new List<Category>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Display], [Status], [PID] FROM [Category]");
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }

        /// <summary>
        /// 通过条件获得满足条件的记录
        /// </summary>
        /// <param name="model">Category类的对象</param>
        /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
        /// <param name="fields">需要的条件的字段名</param>
        /// <returns>满足条件的记录</returns>
        public static IEnumerable<Category> ListByWhere(Category model, string whereStr, params string[] fields)
        {
            List<SqlParameter> lsParameter = new List<SqlParameter>();
            string str = Helper.GenericSQLGenerator.GetWhereStr<Category>(model, "Category", out lsParameter, fields);
            str += whereStr;
            List<Category> list = new List<Category>();
            SqlParameter[] sqlparm = new SqlParameter[lsParameter.Count];
            for (int i = 0; i < lsParameter.Count; i++)
            {
                sqlparm[i] = lsParameter[i];
            }
            DataTable dt = SqlHelper.ExecuteDataTable(str, sqlparm);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }

    }
}
