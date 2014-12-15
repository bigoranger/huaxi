using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace huaxi {
    public static class  UsersDAL {

        public static Users ToModel(DataRow row) {
            Users model = new Users();
            model.ID = (System.Int32)row["ID"];
            model.Name = (System.String)row["Name"];
            model.Password = (System.String)row["Password"];
            model.RegTime = (System.DateTime?)SqlHelper.FromDBValue(row["RegTime"]);
            model.OnlineTime = (System.DateTime?)SqlHelper.FromDBValue(row["OnlineTime"]);
            model.LastLoginTime = (System.DateTime?)SqlHelper.FromDBValue(row["LastLoginTime"]);
            model.RealName = (System.String)SqlHelper.FromDBValue(row["RealName"]);
            model.AvatarID = (System.Int32)row["AvatarID"];
            model.Birthday = (System.DateTime?)SqlHelper.FromDBValue(row["Birthday"]);
            model.Sex = (System.String)SqlHelper.FromDBValue(row["Sex"]);
            model.Address = (System.String)SqlHelper.FromDBValue(row["Address"]);
            model.Telephone = (System.String)SqlHelper.FromDBValue(row["Telephone"]);
            model.QQ = (System.String)SqlHelper.FromDBValue(row["QQ"]);
            model.Sign = (System.String)SqlHelper.FromDBValue(row["Sign"]);
            model.Credit = (System.Int32?)SqlHelper.FromDBValue(row["Credit"]);
            model.RoleID = (System.Int32?)SqlHelper.FromDBValue(row["RoleID"]);
            model.Status = (System.Int32)row["Status"];
            return model;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model">Users类的对象</param>
        /// <returns>插入是否成功</returns>
        public static bool Insert(Users model) {
            int count = SqlHelper.ExecuteNonQuery(@"INSERT INTO [Users]([Name], [Password], [RegTime], [OnlineTime], [LastLoginTime], [RealName], [AvatarID], [Birthday], [Sex], [Address], [Telephone], [QQ], [Sign], [Credit], [RoleID], [Status]) VALUES(@Name, @Password, @RegTime, @OnlineTime, @LastLoginTime, @RealName, @AvatarID, @Birthday, @Sex, @Address, @Telephone, @QQ, @Sign, @Credit, @RoleID, @Status)"
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@Password", model.Password)
            ,new SqlParameter("@RegTime", SqlHelper.ToDBValue(model.RegTime))
            ,new SqlParameter("@OnlineTime", SqlHelper.ToDBValue(model.OnlineTime))
            ,new SqlParameter("@LastLoginTime", SqlHelper.ToDBValue(model.LastLoginTime))
            ,new SqlParameter("@RealName", SqlHelper.ToDBValue(model.RealName))
            ,new SqlParameter("@AvatarID", model.AvatarID)
            ,new SqlParameter("@Birthday", SqlHelper.ToDBValue(model.Birthday))
            ,new SqlParameter("@Sex", SqlHelper.ToDBValue(model.Sex))
            ,new SqlParameter("@Address", SqlHelper.ToDBValue(model.Address))
            ,new SqlParameter("@Telephone", SqlHelper.ToDBValue(model.Telephone))
            ,new SqlParameter("@QQ", SqlHelper.ToDBValue(model.QQ))
            ,new SqlParameter("@Sign", SqlHelper.ToDBValue(model.Sign))
            ,new SqlParameter("@Credit", SqlHelper.ToDBValue(model.Credit))
            ,new SqlParameter("@RoleID", SqlHelper.ToDBValue(model.RoleID))
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
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM [Users] WHERE [ID] = @ID", new SqlParameter("@ID", ID));
            return rows > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model">Users类的对象</param>
        /// <returns>更新是否成功</returns>
        public static bool Update(Users model) {
            int count = SqlHelper.ExecuteNonQuery("UPDATE [Users] SET [Name]=@Name, [Password]=@Password, [RegTime]=@RegTime, [OnlineTime]=@OnlineTime, [LastLoginTime]=@LastLoginTime, [RealName]=@RealName, [AvatarID]=@AvatarID, [Birthday]=@Birthday, [Sex]=@Sex, [Address]=@Address, [Telephone]=@Telephone, [QQ]=@QQ, [Sign]=@Sign, [Credit]=@Credit, [RoleID]=@RoleID, [Status]=@Status WHERE [ID]=@ID"
            ,new SqlParameter("@ID", model.ID)
            ,new SqlParameter("@Name", model.Name)
            ,new SqlParameter("@Password", model.Password)
            ,new SqlParameter("@RegTime", SqlHelper.ToDBValue(model.RegTime))
            ,new SqlParameter("@OnlineTime", SqlHelper.ToDBValue(model.OnlineTime))
            ,new SqlParameter("@LastLoginTime", SqlHelper.ToDBValue(model.LastLoginTime))
            ,new SqlParameter("@RealName", SqlHelper.ToDBValue(model.RealName))
            ,new SqlParameter("@AvatarID", model.AvatarID)
            ,new SqlParameter("@Birthday", SqlHelper.ToDBValue(model.Birthday))
            ,new SqlParameter("@Sex", SqlHelper.ToDBValue(model.Sex))
            ,new SqlParameter("@Address", SqlHelper.ToDBValue(model.Address))
            ,new SqlParameter("@Telephone", SqlHelper.ToDBValue(model.Telephone))
            ,new SqlParameter("@QQ", SqlHelper.ToDBValue(model.QQ))
            ,new SqlParameter("@Sign", SqlHelper.ToDBValue(model.Sign))
            ,new SqlParameter("@Credit", SqlHelper.ToDBValue(model.Credit))
            ,new SqlParameter("@RoleID", SqlHelper.ToDBValue(model.RoleID))
            ,new SqlParameter("@Status", model.Status)
            );
        return count > 0;
        }

        /// <summary>
        /// 获得一条记录
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns>Users类的对象</returns>
        public static Users GetById(System.Int32 ID) {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Password], [RegTime], [OnlineTime], [LastLoginTime], [RealName], [AvatarID], [Birthday], [Sex], [Address], [Telephone], [QQ], [Sign], [Credit], [RoleID], [Status] FROM [Users] WHERE [ID]=@ID", new SqlParameter("@ID", ID));
            if (dt.Rows.Count > 1) {
                throw new Exception("more than 1 row was found");
            }
            else if (dt.Rows.Count <= 0) {
                return null;
            }
            DataRow row = dt.Rows[0];
            Users model = ToModel(row);
            return model;
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns>Users类的对象的枚举</returns>
        public static IEnumerable<Users> ListAll() {
            List<Users> list = new List<Users>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT [ID], [Name], [Password], [RegTime], [OnlineTime], [LastLoginTime], [RealName], [AvatarID], [Birthday], [Sex], [Address], [Telephone], [QQ], [Sign], [Credit], [RoleID], [Status] FROM [Users]");
            foreach (DataRow row in dt.Rows)  {
                list.Add(ToModel(row));
            }
            return list;
        }

      /// <summary>
      /// 通过条件获得满足条件的记录
      /// </summary>
      /// <param name="model">Users类的对象</param>
      /// <param name="whereStr">其他的sql 语句 若果是where只需要“and...  就行了” </param>
      /// <param name="fields">需要的条件的字段名</param>
      /// <returns>满足条件的记录</returns>
       public static IEnumerable<Users> ListByWhere(Users model,string whereStr, params string[] fields)
       {
           List<SqlParameter> lsParameter = new List<SqlParameter>();
           string str = Helper.GenericSQLGenerator.GetWhereStr<Users>(model, "Users", out lsParameter, fields);
           str+=whereStr;
           List<Users> list = new List<Users>();
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
