using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  Users_RulesDAL {

        private static Users_Rules ToModel(DataRow row) {
            Users_Rules model = new Users_Rules();
            model.ID = (System.Int32)row["ID"];
            model.Name = (System.String)SqlHelper.FromDBValue(row["Name"]);
            model.ActionName = (System.String)SqlHelper.FromDBValue(row["ActionName"]);
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Users_Rules类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Users_Rules model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Users_Rules]([ID], [Name], [ActionName], [Status]) VALUES(@ID, @Name, @ActionName, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", SqlHelper.ToDBValue(model.Name))
            ,new SqlParameter("@ActionName", SqlHelper.ToDBValue(model.ActionName))
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Users_Rules] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Users_Rules类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Users_Rules model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Users_Rules] SET [Name]=@Name, [ActionName]=@ActionName, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", SqlHelper.ToDBValue(model.Name))
            ,new SqlParameter("@ActionName", SqlHelper.ToDBValue(model.ActionName))
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Users_Rules类的对象</returns>
        public static Users_Rules GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [ActionName], [Status] FROM [Users_Rules] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Users_Rules model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Users_Rules类的对象的枚举</returns>
        public static IEnumerable<Users_Rules> ListAll() {
            List<Users_Rules> list = new List<Users_Rules>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [ActionName], [Status] FROM [Users_Rules]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Users_Rules类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Users_Rules> ListByWhere(Users_Rules model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Users_Rules>(model, "Users_Rules", out lsParameter, fields);
           str+=whereStr;
           List<Users_Rules> list = new List<Users_Rules>();
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
