using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  Users_RolesInRulesDAL {

        private static Users_RolesInRules ToModel(DataRow row) {
            Users_RolesInRules model = new Users_RolesInRules();
            model.RoleID = (System.Int32)row["RoleID"];
            model.RuleID = (System.Int32)row["RuleID"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Users_RolesInRules类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Users_RolesInRules model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Users_RolesInRules]([RoleID], [RuleID]) VALUES(@RoleID, @RuleID)"
            ,new SqlParameter("@RoleID", model.RoleID)
            ,new SqlParameter("@RuleID", model.RuleID)
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 RoleID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Users_RolesInRules] WHERE [RoleID] = @RoleID", new SqlParameter("@RoleID", RoleID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Users_RolesInRules类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Users_RolesInRules model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Users_RolesInRules] SET [RuleID]=@RuleID WHERE [RoleID]=@RoleID"
            ,new SqlParameter("@RoleID", model.RoleID)
            ,new SqlParameter("@RuleID", model.RuleID)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Users_RolesInRules类的对象</returns>
        public static Users_RolesInRules GetById(System.Int32 RoleID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [RoleID], [RuleID] FROM [Users_RolesInRules] WHERE [RoleID]=@RoleID", new SqlParameter("@RoleID", RoleID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Users_RolesInRules model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Users_RolesInRules类的对象的枚举</returns>
        public static IEnumerable<Users_RolesInRules> ListAll() {
            List<Users_RolesInRules> list = new List<Users_RolesInRules>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [RoleID], [RuleID] FROM [Users_RolesInRules]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Users_RolesInRules类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Users_RolesInRules> ListByWhere(Users_RolesInRules model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Users_RolesInRules>(model, "Users_RolesInRules", out lsParameter, fields);
           str+=whereStr;
           List<Users_RolesInRules> list = new List<Users_RolesInRules>();
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
