using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model.DatabaseConnection
{
    public static class SqlliteUpdateProfile
    {
        public static bool UpdateEmail(UserModel newUser,string email)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    using (SQLiteCommand updateSql = conn.CreateCommand())
                    {
                        
                        updateSql.CommandText = @"UPDATE Uzytkownicy SET Email=@email WHERE Id=@userid";

                        updateSql.Parameters.Add(new SQLiteParameter("@userid", newUser.Id));
                        updateSql.Parameters.Add(new SQLiteParameter("@email", email));
                        updateSql.Connection = conn;
                        conn.Open();
                        int result = updateSql.ExecuteNonQuery();
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
        
        public static bool UpdateFirstName(UserModel loggedUser, string firstname)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    using (SQLiteCommand updateSql = conn.CreateCommand())
                    {

                        updateSql.CommandText = @"UPDATE Uzytkownicy SET Firstname=@firstname WHERE Id=@userid";

                        updateSql.Parameters.Add(new SQLiteParameter("@userid", loggedUser.Id));
                        updateSql.Parameters.Add(new SQLiteParameter("@firstname", firstname));
                        updateSql.Connection = conn;
                        conn.Open();
                        int result = updateSql.ExecuteNonQuery();
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

        public static bool UpdateLastName(UserModel loggedUser, string lastname)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    using (SQLiteCommand updateSql = conn.CreateCommand())
                    {

                        updateSql.CommandText = @"UPDATE Uzytkownicy SET Lastname=@lastname WHERE Id=@userid";

                        updateSql.Parameters.Add(new SQLiteParameter("@userid", loggedUser.Id));
                        updateSql.Parameters.Add(new SQLiteParameter("@lastname", lastname));
                        updateSql.Connection = conn;
                        conn.Open();
                        int result = updateSql.ExecuteNonQuery();
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
