using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model.DatabaseConnection
{
    static public class SqliteInsert
    {
        public static bool RegisterUser(UserModel newUser)
        {
            try
            {
                string passwordhash = Hasher.hashPassword(newUser.Password);
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    using (SQLiteCommand insertSql = conn.CreateCommand())
                    {
                        insertSql.CommandText = @"INSERT INTO Uzytkownicy (Id,Login,Password,Email,Description,Firstname,Lastname) VALUES (null,@userlogin,@userpassword,@useremail,null,@userfname,@userlname)";
                        insertSql.Connection = conn;

                        insertSql.Parameters.Add(new SQLiteParameter("@userlogin", newUser.Login));
                        insertSql.Parameters.Add(new SQLiteParameter("@userpassword", passwordhash));
                        insertSql.Parameters.Add(new SQLiteParameter("@useremail", newUser.Email));
                        insertSql.Parameters.Add(new SQLiteParameter("@userfname", newUser.FirstName));
                        insertSql.Parameters.Add(new SQLiteParameter("@userlname", newUser.LastName));

                        conn.Open();
                        int result = insertSql.ExecuteNonQuery();
                        if (result != -1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
