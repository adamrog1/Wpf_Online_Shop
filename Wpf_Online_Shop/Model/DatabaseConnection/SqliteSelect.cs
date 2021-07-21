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
        /// <summary>
        /// Pobiera danego użytkownika na podstawie wpisanych w formularzu logowania danych
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>UserModel - jeśli użytkownik istnieje <br></br>
        /// null - jeśli nie istnieje w bazie</returns>
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
        /// <summary>
        /// Pobiera listę wszystkich produktów w magazynie, zapisanych w bazie
        /// </summary>
        /// <returns>List obiektów typu ProductModel</returns>
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
        /// <summary>
        /// Zwraca najwyższe Id zamówienia lub null
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Odczytuje stan konta użytkownika w bazie na podstawie jego id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
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
