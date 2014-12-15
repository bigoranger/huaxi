using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  Forum_PostDAL {

        private static Forum_Post ToModel(DataRow row) {
            Forum_Post model = new Forum_Post();
            model.ID = (System.Int32)row["ID"];
            model.ForumID = (System.Int32)row["ForumID"];
            model.UserID = (System.Int32)row["UserID"];
            model.Title = (System.String)row["Title"];
            model.Contents = (System.String)row["Contents"];
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.UpdateTime = (System.DateTime?)SqlHelper.FromDBValue(row["UpdateTime"]);
            model.LastReplyTime = (System.DateTime?)SqlHelper.FromDBValue(row["LastReplyTime"]);
            model.Views = (System.Int32)row["Views"];
            model.ReplyCount = (System.Int32)row["ReplyCount"];
            model.IsTop = (System.Boolean)row["IsTop"];
            model.Status = (System.String)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Forum_Post类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Forum_Post model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Forum_Post]([ID], [ForumID], [UserID], [Title], [Contents], [CreateTime], [UpdateTime], [LastReplyTime], [Views], [ReplyCount], [IsTop], [Status]) VALUES(@ID, @ForumID, @UserID, @Title, @Contents, @CreateTime, @UpdateTime, @LastReplyTime, @Views, @ReplyCount, @IsTop, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@ForumID", model.ForumID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@Title", model.Title)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@UpdateTime", SqlHelper.ToDBValue(model.UpdateTime))
            ,new SqlParameter("@LastReplyTime", SqlHelper.ToDBValue(model.LastReplyTime))
            ,new SqlParameter("@Views", model.Views)
            ,new SqlParameter("@ReplyCount", model.ReplyCount)
            ,new SqlParameter("@IsTop", model.IsTop)
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Forum_Post] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Forum_Post类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Forum_Post model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Forum_Post] SET [ForumID]=@ForumID, [UserID]=@UserID, [Title]=@Title, [Contents]=@Contents, [CreateTime]=@CreateTime, [UpdateTime]=@UpdateTime, [LastReplyTime]=@LastReplyTime, [Views]=@Views, [ReplyCount]=@ReplyCount, [IsTop]=@IsTop, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@ForumID", model.ForumID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@Title", model.Title)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@UpdateTime", SqlHelper.ToDBValue(model.UpdateTime))
            ,new SqlParameter("@LastReplyTime", SqlHelper.ToDBValue(model.LastReplyTime))
            ,new SqlParameter("@Views", model.Views)
            ,new SqlParameter("@ReplyCount", model.ReplyCount)
            ,new SqlParameter("@IsTop", model.IsTop)
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Forum_Post类的对象</returns>
        public static Forum_Post GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [ForumID], [UserID], [Title], [Contents], [CreateTime], [UpdateTime], [LastReplyTime], [Views], [ReplyCount], [IsTop], [Status] FROM [Forum_Post] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Forum_Post model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Forum_Post类的对象的枚举</returns>
        public static IEnumerable<Forum_Post> ListAll() {
            List<Forum_Post> list = new List<Forum_Post>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [ForumID], [UserID], [Title], [Contents], [CreateTime], [UpdateTime], [LastReplyTime], [Views], [ReplyCount], [IsTop], [Status] FROM [Forum_Post]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Forum_Post类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Forum_Post> ListByWhere(Forum_Post model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Forum_Post>(model, "Forum_Post", out lsParameter, fields);
           str+=whereStr;
           List<Forum_Post> list = new List<Forum_Post>();
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
