using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  Document_ArticleDAL {

        private static Document_Article ToModel(DataRow row) {
            Document_Article model = new Document_Article();
            model.DocID = (System.Int32)row["DocID"];
            model.Contents = (System.String)row["Contents"];
            model.BeFrom = (System.String)SqlHelper.FromDBValue(row["BeFrom"]);
            model.ExtendContents = (System.String)SqlHelper.FromDBValue(row["ExtendContents"]);
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Document_Article类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Document_Article model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Document_Article]([DocID], [Contents], [BeFrom], [ExtendContents]) VALUES(@DocID, @Contents, @BeFrom, @ExtendContents)"
            ,new SqlParameter("@DocID", model.DocID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@BeFrom", SqlHelper.ToDBValue(model.BeFrom))
            ,new SqlParameter("@ExtendContents", SqlHelper.ToDBValue(model.ExtendContents))
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 DocID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Document_Article] WHERE [DocID] = @DocID", new SqlParameter("@DocID", DocID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Document_Article类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Document_Article model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Document_Article] SET [Contents]=@Contents, [BeFrom]=@BeFrom, [ExtendContents]=@ExtendContents WHERE [DocID]=@DocID"
            ,new SqlParameter("@DocID", model.DocID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@BeFrom", SqlHelper.ToDBValue(model.BeFrom))
            ,new SqlParameter("@ExtendContents", SqlHelper.ToDBValue(model.ExtendContents))
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Document_Article类的对象</returns>
        public static Document_Article GetById(System.Int32 DocID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [DocID], [Contents], [BeFrom], [ExtendContents] FROM [Document_Article] WHERE [DocID]=@DocID", new SqlParameter("@DocID", DocID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Document_Article model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Document_Article类的对象的枚举</returns>
        public static IEnumerable<Document_Article> ListAll() {
            List<Document_Article> list = new List<Document_Article>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [DocID], [Contents], [BeFrom], [ExtendContents] FROM [Document_Article]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Document_Article类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Document_Article> ListByWhere(Document_Article model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Document_Article>(model, "Document_Article", out lsParameter, fields);
           str+=whereStr;
           List<Document_Article> list = new List<Document_Article>();
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
