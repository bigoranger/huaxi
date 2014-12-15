using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  Document_GoodsDAL {

        private static Document_Goods ToModel(DataRow row) {
            Document_Goods model = new Document_Goods();
            model.DocID = (System.Int32)row["DocID"];
            model.Contents = (System.String)row["Contents"];
            model.PictureID = (System.Int32?)SqlHelper.FromDBValue(row["PictureID"]);
            model.Price = (System.Int32)row["Price"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Document_Goods类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Document_Goods model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Document_Goods]([DocID], [Contents], [PictureID], [Price]) VALUES(@DocID, @Contents, @PictureID, @Price)"
            ,new SqlParameter("@DocID", model.DocID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@PictureID", SqlHelper.ToDBValue(model.PictureID))
            ,new SqlParameter("@Price", model.Price)
            );
        return count > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 DocID) {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Document_Goods] WHERE [DocID] = @DocID", new SqlParameter("@DocID", DocID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Document_Goods类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Document_Goods model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Document_Goods] SET [Contents]=@Contents, [PictureID]=@PictureID, [Price]=@Price WHERE [DocID]=@DocID"
            ,new SqlParameter("@DocID", model.DocID)
            ,new SqlParameter("@Contents", model.Contents)
            ,new SqlParameter("@PictureID", SqlHelper.ToDBValue(model.PictureID))
            ,new SqlParameter("@Price", model.Price)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Document_Goods类的对象</returns>
        public static Document_Goods GetById(System.Int32 DocID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [DocID], [Contents], [PictureID], [Price] FROM [Document_Goods] WHERE [DocID]=@DocID", new SqlParameter("@DocID", DocID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Document_Goods model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Document_Goods类的对象的枚举</returns>
        public static IEnumerable<Document_Goods> ListAll() {
            List<Document_Goods> list = new List<Document_Goods>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [DocID], [Contents], [PictureID], [Price] FROM [Document_Goods]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Document_Goods类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Document_Goods> ListByWhere(Document_Goods model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Document_Goods>(model, "Document_Goods", out lsParameter, fields);
           str+=whereStr;
           List<Document_Goods> list = new List<Document_Goods>();
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
