using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  CreditRulesDAL {

        private static CreditRules ToModel(DataRow row) {
            CreditRules model = new CreditRules();
            model.ID = (System.Int32)row["ID"];
            model.Name = (System.String)row["Name"];
            model.Remark = (System.String)SqlHelper.FromDBValue(row["Remark"]);
            model.Credit = (System.Int32)row["Credit"];
            model.TodayMax = (System.Int32)row["TodayMax"];
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">CreditRules类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(CreditRules model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [CreditRules]([ID], [Name], [Remark], [Credit], [TodayMax], [Status]) VALUES(@ID, @Name, @Remark, @Credit, @TodayMax, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@Remark", SqlHelper.ToDBValue(model.Remark))
            ,new SqlParameter("@Credit", model.Credit)
            ,new SqlParameter("@TodayMax", model.TodayMax)
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [CreditRules] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">CreditRules类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(CreditRules model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [CreditRules] SET [Name]=@Name, [Remark]=@Remark, [Credit]=@Credit, [TodayMax]=@TodayMax, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@Remark", SqlHelper.ToDBValue(model.Remark))
            ,new SqlParameter("@Credit", model.Credit)
            ,new SqlParameter("@TodayMax", model.TodayMax)
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>CreditRules类的对象</returns>
        public static CreditRules GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Remark], [Credit], [TodayMax], [Status] FROM [CreditRules] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            CreditRules model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>CreditRules类的对象的枚举</returns>
        public static IEnumerable<CreditRules> ListAll() {
            List<CreditRules> list = new List<CreditRules>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Remark], [Credit], [TodayMax], [Status] FROM [CreditRules]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">CreditRules类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<CreditRules> ListByWhere(CreditRules model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<CreditRules>(model, "CreditRules", out lsParameter, fields);
           str+=whereStr;
           List<CreditRules> list = new List<CreditRules>();
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
