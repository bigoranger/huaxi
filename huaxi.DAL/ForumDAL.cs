using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  ForumDAL {

        private static Forum ToModel(DataRow row) {
            Forum model = new Forum();
            model.ID = (System.Int32)row["ID"];
            model.Title = (System.String)SqlHelper.FromDBValue(row["Title"]);
            model.Description = (System.String)SqlHelper.FromDBValue(row["Description"]);
            model.CreateTime = (System.DateTime?)SqlHelper.FromDBValue(row["CreateTime"]);
            model.PostCount = (System.Int32)row["PostCount"];
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Forum类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Forum model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Forum]([ID], [Title], [Description], [CreateTime], [PostCount], [Status]) VALUES(@ID, @Title, @Description, @CreateTime, @PostCount, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Title", SqlHelper.ToDBValue(model.Title))
            ,new SqlParameter("@Description", SqlHelper.ToDBValue(model.Description))
            ,new SqlParameter("@CreateTime", SqlHelper.ToDBValue(model.CreateTime))
            ,new SqlParameter("@PostCount", model.PostCount)
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 ID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Forum] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Forum类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Forum model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Forum] SET [Title]=@Title, [Description]=@Description, [CreateTime]=@CreateTime, [PostCount]=@PostCount, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Title", SqlHelper.ToDBValue(model.Title))
            ,new SqlParameter("@Description", SqlHelper.ToDBValue(model.Description))
            ,new SqlParameter("@CreateTime", SqlHelper.ToDBValue(model.CreateTime))
            ,new SqlParameter("@PostCount", model.PostCount)
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Forum类的对象</returns>
        public static Forum GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Title], [Description], [CreateTime], [PostCount], [Status] FROM [Forum] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Forum model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Forum类的对象的枚举</returns>
        public static IEnumerable<Forum> ListAll() {
            List<Forum> list = new List<Forum>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Title], [Description], [CreateTime], [PostCount], [Status] FROM [Forum]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Forum类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Forum> ListByWhere(Forum model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Forum>(model, "Forum", out lsParameter, fields);
           str+=whereStr;
           List<Forum> list = new List<Forum>();
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
