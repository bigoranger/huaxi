using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  MessageDAL {

        private static Message ToModel(DataRow row) {
            Message model = new Message();
            model.ID = (System.Int32)row["ID"];
            model.ToUserID = (System.Int32)row["ToUserID"];
            model.FromUserID = (System.Int32)row["FromUserID"];
            model.Contents = (System.String)row["Contents"];
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.IsRead = (System.Boolean)row["IsRead"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Message类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Message model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Message]([ID], [ToUserID], [FromUserID], [Contents], [CreateTime], [IsRead]) VALUES(@ID, @ToUserID, @FromUserID, @Contents, @CreateTime, @IsRead)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@ToUserID", model.ToUserID)
            ,new SqlParameter("@FromUserID", model.FromUserID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@IsRead", model.IsRead)
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 ID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Message] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Message类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Message model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Message] SET [ToUserID]=@ToUserID, [FromUserID]=@FromUserID, [Contents]=@Contents, [CreateTime]=@CreateTime, [IsRead]=@IsRead WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@ToUserID", model.ToUserID)
            ,new SqlParameter("@FromUserID", model.FromUserID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@IsRead", model.IsRead)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Message类的对象</returns>
        public static Message GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [ToUserID], [FromUserID], [Contents], [CreateTime], [IsRead] FROM [Message] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Message model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Message类的对象的枚举</returns>
        public static IEnumerable<Message> ListAll() {
            List<Message> list = new List<Message>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [ToUserID], [FromUserID], [Contents], [CreateTime], [IsRead] FROM [Message]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Message类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Message> ListByWhere(Message model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Message>(model, "Message", out lsParameter, fields);
           str+=whereStr;
           List<Message> list = new List<Message>();
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
