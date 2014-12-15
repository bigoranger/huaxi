using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  Forum_Post_ReplyDAL {

        private static Forum_Post_Reply ToModel(DataRow row) {
            Forum_Post_Reply model = new Forum_Post_Reply();
            model.ID = (System.Int32)row["ID"];
            model.PostID = (System.Int32)row["PostID"];
            model.UserID = (System.Int32)row["UserID"];
            model.Contents = (System.String)row["Contents"];
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.UpdateTime = (System.DateTime?)SqlHelper.FromDBValue(row["UpdateTime"]);
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Forum_Post_Reply类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Forum_Post_Reply model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Forum_Post_Reply]([ID], [PostID], [UserID], [Contents], [CreateTime], [UpdateTime], [Status]) VALUES(@ID, @PostID, @UserID, @Contents, @CreateTime, @UpdateTime, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@PostID", model.PostID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@UpdateTime", SqlHelper.ToDBValue(model.UpdateTime))
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Forum_Post_Reply] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Forum_Post_Reply类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Forum_Post_Reply model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Forum_Post_Reply] SET [PostID]=@PostID, [UserID]=@UserID, [Contents]=@Contents, [CreateTime]=@CreateTime, [UpdateTime]=@UpdateTime, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@PostID", model.PostID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@UpdateTime", SqlHelper.ToDBValue(model.UpdateTime))
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Forum_Post_Reply类的对象</returns>
        public static Forum_Post_Reply GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [PostID], [UserID], [Contents], [CreateTime], [UpdateTime], [Status] FROM [Forum_Post_Reply] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Forum_Post_Reply model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Forum_Post_Reply类的对象的枚举</returns>
        public static IEnumerable<Forum_Post_Reply> ListAll() {
            List<Forum_Post_Reply> list = new List<Forum_Post_Reply>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [PostID], [UserID], [Contents], [CreateTime], [UpdateTime], [Status] FROM [Forum_Post_Reply]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Forum_Post_Reply类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Forum_Post_Reply> ListByWhere(Forum_Post_Reply model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Forum_Post_Reply>(model, "Forum_Post_Reply", out lsParameter, fields);
           str+=whereStr;
           List<Forum_Post_Reply> list = new List<Forum_Post_Reply>();
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
