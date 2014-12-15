using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  LogsDAL {

        private static Logs ToModel(DataRow row) {
            Logs model = new Logs();
            model.ID = (System.Int64)row["ID"];
            model.UserID = (System.Int32)row["UserID"];
            model.CreditID = (System.Int32?)SqlHelper.FromDBValue(row["CreditID"]);
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.UserIP = (System.String)row["UserIP"];
            model.Remark = (System.String)row["Remark"];
            model.ActionName = (System.String)row["ActionName"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Logs类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Logs model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Logs]([ID], [UserID], [CreditID], [CreateTime], [UserIP], [Remark], [ActionName]) VALUES(@ID, @UserID, @CreditID, @CreateTime, @UserIP, @Remark, @ActionName)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@CreditID", SqlHelper.ToDBValue(model.CreditID))
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@UserIP", model.UserIP)
            ,new SqlParameter("@Remark", model.Remark)
            ,new SqlParameter("@ActionName", model.ActionName)
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int64 ID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Logs] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Logs类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Logs model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Logs] SET [UserID]=@UserID, [CreditID]=@CreditID, [CreateTime]=@CreateTime, [UserIP]=@UserIP, [Remark]=@Remark, [ActionName]=@ActionName WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@UserID", model.UserID)
            ,new SqlParameter("@CreditID", SqlHelper.ToDBValue(model.CreditID))
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@UserIP", model.UserIP)
            ,new SqlParameter("@Remark", model.Remark)
            ,new SqlParameter("@ActionName", model.ActionName)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Logs类的对象</returns>
        public static Logs GetById(System.Int64 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [UserID], [CreditID], [CreateTime], [UserIP], [Remark], [ActionName] FROM [Logs] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Logs model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Logs类的对象的枚举</returns>
        public static IEnumerable<Logs> ListAll() {
            List<Logs> list = new List<Logs>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [UserID], [CreditID], [CreateTime], [UserIP], [Remark], [ActionName] FROM [Logs]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Logs类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Logs> ListByWhere(Logs model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Logs>(model, "Logs", out lsParameter, fields);
           str+=whereStr;
           List<Logs> list = new List<Logs>();
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
