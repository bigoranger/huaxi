using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi
{
    public static class DocumentsDAL
    {

        private static Documents ToModel(DataRow row)
        {
            Documents model = new Documents();
            model.ID = (System.Int32)row["ID"];
            model.UserName = (System.String)row["UserName"];
            model.Title = (System.String)row["Title"];
            model.Description = (System.String)SqlHelper.FromDBValue(row["Description"]);
            model.CateID = (System.Int32)row["CateID"];
            model.CreateTime = (System.DateTime)row["CreateTime"];
            model.UpdateTime = (System.DateTime)row["UpdateTime"];
            model.Comment = (System.Int32)row["Comment"];
            model.Views = (System.Int32)row["Views"];
            model.CoverID = (System.Int32)row["CoverID"];
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Documents类的对象</param>
        /// <returns>插入是否成功</returns>
        public static int Insert(Documents model)
        {
            DataTable dt = SqlHelper.ExecuteDataTable(@"INSERT INTO [Documents]( [UserName], [Title], [Description], [CateID], [CreateTime], [UpdateTime], [Comment], [Views], [CoverID], [Status]) VALUES( @UserName, @Title, @Description, @CateID, @CreateTime, @UpdateTime, @Comment, @Views, @CoverID, @Status)
select @@identity"
            , new SqlParameter("@UserName", model.UserName)
            , new SqlParameter("@Title", model.Title)
            , new SqlParameter("@Description", SqlHelper.ToDBValue(model.Description))
            , new SqlParameter("@CateID", model.CateID)
            , new SqlParameter("@CreateTime", model.CreateTime)
            , new SqlParameter("@UpdateTime", model.UpdateTime)
            , new SqlParameter("@Comment", model.Comment)
            , new SqlParameter("@Views", model.Views)
            , new SqlParameter("@CoverID", model.CoverID)
            , new SqlParameter("@Status", model.Status)
            );
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteById(System.Int32 ID)
        {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Documents] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Documents类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Documents model)
        {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Documents] SET [UserName]=@UserName, [Title]=@Title, [Description]=@Description, [CateID]=@CateID, [CreateTime]=@CreateTime, [UpdateTime]=@UpdateTime, [Comment]=@Comment, [Views]=@Views, [CoverID]=@CoverID, [Status]=@Status WHERE [ID]=@ID"
            , new SqlParameter("@ID", model.ID)
            , new SqlParameter("@UserName", model.UserName)
            , new SqlParameter("@Title", model.Title)
            , new SqlParameter("@Description", SqlHelper.ToDBValue(model.Description))
            , new SqlParameter("@CateID", model.CateID)
            , new SqlParameter("@CreateTime", model.CreateTime)
            , new SqlParameter("@UpdateTime", model.UpdateTime)
            , new SqlParameter("@Comment", model.Comment)
            , new SqlParameter("@Views", model.Views)
            , new SqlParameter("@CoverID", model.CoverID)
            , new SqlParameter("@Status", model.Status)
            );
            return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Documents类的对象</returns>
        public static Documents GetById(System.Int32 ID)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [UserName], [Title], [Description], [CateID], [CreateTime], [UpdateTime], [Comment], [Views], [CoverID], [Status] FROM [Documents] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            Documents model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Documents类的对象的枚举</returns>
        public static IEnumerable<Documents> ListAll()
        {
            List<Documents> list = new List<Documents>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [UserName], [Title], [Description], [CateID], [CreateTime], [UpdateTime], [Comment], [Views], [CoverID], [Status] FROM [Documents]");
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }

        /// <summary>
        /// 通过条件获得满足条件的记录
        /// </summary>
        /// <param name="model">Documents类的对象</param>
        /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
        /// <param name="fields">需要的条件的字段名</param>
        /// <returns>满足条件的记录</returns>
        public static IEnumerable<Documents> ListByWhere(Documents model, string whereStr, params string[] fields)
        {
            List<SqlParameter> lsParameter = new List<SqlParameter>();
            string str = Helper.GenericSQLGenerator.GetWhereStr<Documents>(model, "Documents", out lsParameter, fields);
            str += whereStr;
            List<Documents> list = new List<Documents>();
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
