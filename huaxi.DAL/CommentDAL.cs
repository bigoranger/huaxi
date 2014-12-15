using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  CommentDAL {

        private static Comment ToModel(DataRow row) {
            Comment model = new Comment();
            model.ID = (System.Int32)row["ID"];
            model.DocID = (System.Int32)row["DocID"];
            model.UserID = (System.Int32)row["UserID"];
            model.Contents = (System.String)row["Contents"];
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Comment类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Comment model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Comment]([ID], [DocID], [UserID], [Contents], [CreateTime], [Status]) VALUES(@ID, @DocID, @UserID, @Contents, @CreateTime, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@DocID", model.DocID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Comment] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Comment类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Comment model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Comment] SET [DocID]=@DocID, [UserID]=@UserID, [Contents]=@Contents, [CreateTime]=@CreateTime, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@DocID", model.DocID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Comment类的对象</returns>
        public static Comment GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [DocID], [UserID], [Contents], [CreateTime], [Status] FROM [Comment] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Comment model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Comment类的对象的枚举</returns>
        public static IEnumerable<Comment> ListAll() {
            List<Comment> list = new List<Comment>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [DocID], [UserID], [Contents], [CreateTime], [Status] FROM [Comment]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Comment类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Comment> ListByWhere(Comment model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Comment>(model, "Comment", out lsParameter, fields);
           str+=whereStr;
           List<Comment> list = new List<Comment>();
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
