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
            try
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
            catch
            {
                throw;
            }
        }

        public static List<ProductModel> GetProducts()
        {
            try
            {
                List<ProductModel> products = new List<ProductModel>();
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    conn.Open();
                    using (SQLiteCommand res = conn.CreateCommand())
                    {
                        string query = Queries.SelectQueries.getProducts();
                        res.CommandText = query;
                        SQLiteDataReader r = res.ExecuteReader();
                        while (r.Read())
                        {
                            products.Add(Converter.ParseModel.GetProductFromSqliteRecord(r));
                        }
                        conn.Close();
                    }
                }
                return products;
            }
            catch
            {
                throw;
            }
        }

        public static int? GetOrderId()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    conn.Open();
                    int? id = null;
                    using (SQLiteCommand res = conn.CreateCommand())
                    {
                        string query = $"select max(Id) as Id from Orders";
                        res.CommandText = query;
                        SQLiteDataReader r = res.ExecuteReader();
                        while (r.Read())
                        {
                            if (r["Id"].GetType() == typeof(DBNull))
                            {
                                id = 0;
                            }
                            else
                            {
                                id = Convert.ToInt32(r["Id"]);
                            }
                        }
                        conn.Close();
                    }
                    if (id <= 0) return null;
                    return id;
                }
            }
            catch
            {
                throw;
            }
        }

        public static int GetUserCash(int userid)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    conn.Open();
                    int cash = 0;
                    using (SQLiteCommand res = conn.CreateCommand())
                    {
                        string query = $"select Cash from Uzytkownicy where id = {userid}";
                        res.CommandText = query;
                        SQLiteDataReader r = res.ExecuteReader();
                        while (r.Read())
                        {
                            if (r[0].GetType() == typeof(DBNull))
                            {
                                cash = 0;
                            }
                            else
                            {
                                cash = Convert.ToInt32(r[0]);
                            }
                        }
                        conn.Close();
                    }
                    if (cash <= 0) return 0;
                    return cash;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
