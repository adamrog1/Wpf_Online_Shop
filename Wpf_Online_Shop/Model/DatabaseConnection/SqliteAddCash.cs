using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model.DatabaseConnection
{
    static public class SqliteAddCash
    {
        public static bool AddCash(UserModel newUser, int ammount)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    using (SQLiteCommand sqladdcash = conn.CreateCommand())
                    {

                        sqladdcash.CommandText = @"UPDATE Uzytkownicy SET Cash=Cash+@cash WHERE id=@userid";
                        sqladdcash.Parameters.Add(new SQLiteParameter("@cash", ammount));
                        sqladdcash.Parameters.Add(new SQLiteParameter("@userid", newUser.Id));
                        conn.Open();
                        int result = sqladdcash.ExecuteNonQuery();
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
