using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Online_Shop.Model.DatabaseConnection
{
    static class SqliteSelect
    {
        public static UserModel GetUserByLogin(string login, string password)
        {
            string passwordhash = Hasher.hashPassword(password);
            using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
            {
                conn.Open();
                UserModel user = null;
                using (SQLiteCommand res = conn.CreateCommand())
                {
                    string query = $"SELECT * from Uzytkownicy  where Login = '{login}' and Password = '{passwordhash}'";
                    res.CommandText = query;
                    SQLiteDataReader r = res.ExecuteReader();
                    while (r.Read())
                    {
                        user = Converter.ParseModel.getUserModelFromSqliteRecord(r);  
                    }
                    conn.Close();
                }
                return user;
            }
        }
    }
}
