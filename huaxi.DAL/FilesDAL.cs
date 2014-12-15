using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  FilesDAL {

        private static Files ToModel(DataRow row) {
            Files model = new Files();
            model.ID = (System.Int32)row["ID"];
            model.Name = (System.String)row["Name"];
            model.SaveName = (System.String)row["SaveName"];
            model.SavePath = (System.String)row["SavePath"];
            model.Ext = (System.String)row["Ext"];
            model.Mime = (System.String)row["Mime"];
            model.Size = (System.Int32)row["Size"];
            model.MD5 = (System.String)row["MD5"];
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Files类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Files model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Files]([ID], [Name], [SaveName], [SavePath], [Ext], [Mime], [Size], [MD5], [CreateTime], [Status]) VALUES(@ID, @Name, @SaveName, @SavePath, @Ext, @Mime, @Size, @MD5, @CreateTime, @Status)"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@SaveName", model.SaveName)
            ,new SqlParameter("@SavePath", model.SavePath)
            ,new SqlParameter("@Ext", model.Ext)
            ,new SqlParameter("@Mime", model.Mime)
            ,new SqlParameter("@Size", model.Size)
            ,new SqlParameter("@MD5", model.MD5)
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Files] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Files类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Files model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Files] SET [Name]=@Name, [SaveName]=@SaveName, [SavePath]=@SavePath, [Ext]=@Ext, [Mime]=@Mime, [Size]=@Size, [MD5]=@MD5, [CreateTime]=@CreateTime, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@SaveName", model.SaveName)
            ,new SqlParameter("@SavePath", model.SavePath)
            ,new SqlParameter("@Ext", model.Ext)
            ,new SqlParameter("@Mime", model.Mime)
            ,new SqlParameter("@Size", model.Size)
            ,new SqlParameter("@MD5", model.MD5)
            ,new SqlParameter("@CreateTime", model.CreateTime)
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Files类的对象</returns>
        public static Files GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [SaveName], [SavePath], [Ext], [Mime], [Size], [MD5], [CreateTime], [Status] FROM [Files] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Files model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Files类的对象的枚举</returns>
        public static IEnumerable<Files> ListAll() {
            List<Files> list = new List<Files>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [SaveName], [SavePath], [Ext], [Mime], [Size], [MD5], [CreateTime], [Status] FROM [Files]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Files类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Files> ListByWhere(Files model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Files>(model, "Files", out lsParameter, fields);
           str+=whereStr;
           List<Files> list = new List<Files>();
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
