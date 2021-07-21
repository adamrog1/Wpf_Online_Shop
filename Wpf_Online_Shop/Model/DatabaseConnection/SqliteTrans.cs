using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Online_Shop.Model.DatabaseConnection
{
    public static class SqliteTrans
    {
        /// <summary>
        /// Transakcja, wykonująca operację zamówienia. Wstawia do odpowiednich tabel log zamówienia, kupione produkty i ich ilość, <br></br>
        /// zmienia ilość produktów w magazynie oraz pobiera pieniądze z konta użytkownika.<br></br>
        /// Transakcja nie zostanie wykonana, jeśli któraś z czynności zostanie wykonana z błędem.
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns>bool - wskazuje, czy transakcja wykonała się pomyślnie</returns>
        public static bool OrderInsertTransaction(OrderModel orderModel)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(SqliteConnectionSetup.LoadConnectionString()))
                {
                    conn.Open();
                    SQLiteCommand insertSql = new SQLiteCommand();
                    insertSql.Connection = conn;
                    SQLiteTransaction sqlTrans;
                    sqlTrans = conn.BeginTransaction();
                    insertSql.Transaction = sqlTrans;
                    try
                    {
                        //INSERT INTO ORDERS
                        insertSql.CommandText = @"INSERT INTO Orders (Id,UserId,Date,Street,House,Apartment,Postcode,City,Country,Firstname,Lastname,Cost) VALUES (@orderid,@userid,@date,@street,@house,@apartment,@postcode,@city,@country,@firstname,@lastname,@cost)";
                        insertSql.Connection = conn;

                        insertSql.Parameters.Add(new SQLiteParameter("@orderid", orderModel.Id));
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

                        insertSql.ExecuteNonQuery();

                        //INSERT ITEMS RECORDS
                        foreach(CartItemModel el in orderModel.ListofProducts)
                        {
                            //ADD RECORD 

                            SQLiteCommand insertItem = new SQLiteCommand();
                            insertItem.Connection = conn;
                            insertItem.Transaction = sqlTrans;

                            insertItem.CommandText = "INSERT INTO Orders_items (Id,OrderId,ProductId,Price,Amount) VALUES (null,@orderid,@prodid,@price,@amount)";
                            insertItem.Parameters.Add(new SQLiteParameter("@orderid", orderModel.Id));
                            insertItem.Parameters.Add(new SQLiteParameter("@prodid", el.Product.Id));
                            insertItem.Parameters.Add(new SQLiteParameter("@price", el.Product.PriceGrosz));
                            insertItem.Parameters.Add(new SQLiteParameter("@amount", el.CartAmount));
                            insertItem.ExecuteNonQuery();

                            //CHANGE PRODUCT AMOUNT IN STORAGE

                            SQLiteCommand updateProductAmount = new SQLiteCommand();
                            updateProductAmount.Connection = conn;
                            updateProductAmount.Transaction = sqlTrans;

                            updateProductAmount.CommandText = $"UPDATE Produkty SET Amount = Amount - {el.CartAmount} WHERE id = {el.Product.Id}";
                            updateProductAmount.ExecuteNonQuery();

                        }

                        //Take away money from user's account
                        SQLiteCommand updateUserCash = new SQLiteCommand();
                        updateUserCash.Connection = conn;
                        updateUserCash.Transaction = sqlTrans;

                        updateUserCash.CommandText = $"UPDATE Uzytkownicy SET Cash = Cash-{orderModel.Cost} where id = {orderModel.UserId}";
                        updateUserCash.ExecuteNonQuery();

                        //If no errors - commit transaction
                        sqlTrans.Commit();
                        conn.Close();
                        return true;
                    }
                    catch(Exception e)
                    {
                        sqlTrans.Rollback();
                        MessageBox.Show(e.Message);
                        conn.Close();
                        return false;
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
