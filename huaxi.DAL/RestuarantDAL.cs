using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  RestuarantDAL {

        private static Restuarant ToModel(DataRow row) {
            Restuarant model = new Restuarant();
            model.ID = (System.Int32)row["ID"];
            model.Name = (System.String)row["Name"];
            model.Telephone = (System.String)SqlHelper.FromDBValue(row["Telephone"]);
            model.Address = (System.String)SqlHelper.FromDBValue(row["Address"]);
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Restuarant类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Restuarant model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Restuarant]([ID], [Name], [Telephone], [Address]) VALUES(@ID, @Name, @Telephone, @Address)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@Telephone", SqlHelper.ToDBValue(model.Telephone))
            ,new SqlParameter("@Address", SqlHelper.ToDBValue(model.Address))
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 ID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Restuarant] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Restuarant类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Restuarant model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Restuarant] SET [Name]=@Name, [Telephone]=@Telephone, [Address]=@Address WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@Telephone", SqlHelper.ToDBValue(model.Telephone))
            ,new SqlParameter("@Address", SqlHelper.ToDBValue(model.Address))
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Restuarant类的对象</returns>
        public static Restuarant GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Telephone], [Address] FROM [Restuarant] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Restuarant model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Restuarant类的对象的枚举</returns>
        public static IEnumerable<Restuarant> ListAll() {
            List<Restuarant> list = new List<Restuarant>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Telephone], [Address] FROM [Restuarant]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Restuarant类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Restuarant> ListByWhere(Restuarant model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Restuarant>(model, "Restuarant", out lsParameter, fields);
           str+=whereStr;
           List<Restuarant> list = new List<Restuarant>();
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
