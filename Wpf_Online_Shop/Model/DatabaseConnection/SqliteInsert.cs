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
        /// <summary>
        /// Metoda do rejestrowania użytkownika na podstawie gotowego usermodelu
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
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

        public static bool InsertOrderRecord(OrderModel orderModel)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    using (SQLiteCommand insertSql = conn.CreateCommand())
                    {
                        
                        insertSql.CommandText = @"INSERT INTO Orders (Id,UserId,Date,Street,House,Apartment,Postcode,City,Country,Firstname,Lastname,Cost) VALUES (null,@userid,@date,@street,@house,@apartment,@postcode,@city,@country,@firstname,@lastname,@cost)";
                        insertSql.Connection = conn;

                        insertSql.Parameters.Add(new SQLiteParameter("@userid", orderModel.UserId));
                        insertSql.Parameters.Add(new SQLiteParameter("@date", orderModel.GetDateAsText));
                        insertSql.Parameters.Add(new SQLiteParameter("@street", orderModel.Street));
                        insertSql.Parameters.Add(new SQLiteParameter("@house", orderModel.House));
                        insertSql.Parameters.Add(new SQLiteParameter("@apartment", orderModel.Apartment));
                        insertSql.Parameters.Add(new SQLiteParameter("@postcode", orderModel.Postcode));
                        insertSql.Parameters.Add(new SQLiteParameter("@city", orderModel.City));
                        insertSql.Parameters.Add(new SQLiteParameter("@country", orderModel.Country));
                        insertSql.Parameters.Add(new SQLiteParameter("@firstname", orderModel.FirstName));
                        insertSql.Parameters.Add(new SQLiteParameter("@lastname", orderModel.LastName));
                        insertSql.Parameters.Add(new SQLiteParameter("@cost", orderModel.Cost));
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
