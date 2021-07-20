using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model.Converter
{
    static public class ParseModel
    {
        public static UserModel getUserModelFromSqliteRecord(SQLiteDataReader r)
        {
            int id = Convert.ToInt32(r["Id"]);
            string login = Convert.ToString(r["Login"]);
            string email = Convert.ToString(r["Email"]);
            string desc = Convert.ToString(r["Description"]);
            string firstname = Convert.ToString(r["Firstname"]);
            string lastname = Convert.ToString(r["Lastname"]);
            int cash = Convert.ToInt32(r["Cash"]);
            string phone = Convert.ToString(r["Phone"]);

            UserModel um = new UserModel();
            um.Id = id;
            um.Login = login;
            um.Email = email;
            um.Description = desc;
            um.FirstName = firstname;
            um.LastName = lastname;
            um.Password = null;
            um.Cash = cash;
            um.Phone = phone;
            return um;
        }

        public static ProductModel GetProductFromSqliteRecord(SQLiteDataReader r)
        {
            int id = Convert.ToInt32(r["Id"]);
            string name = Convert.ToString(r["Name"]);
            int category = Convert.ToInt32(r["Category"]);
            string description = Convert.ToString(r["Description"]);
            string manufacturer = Convert.ToString(r["Manufacturer"]);
            string address = Convert.ToString(r["Address"]);
            string city = Convert.ToString(r["City"]);
            string country = Convert.ToString(r["Country"]);

            int PLN = Convert.ToInt32(r["PLN"]);
            int grosz = Convert.ToInt32(r["Grosz"]);
            int amount = Convert.ToInt32(r["Amount"]);

            ProductModel pm = new ProductModel();
            pm.Id = id;
            pm.Name = name;
            pm.Category = category;
            pm.Description = description;
            pm.Manufacturer = manufacturer;
            pm.Address = address;
            pm.City = city;
            pm.Country = country;
            pm.PLN = PLN;
            pm.Grosz = grosz;
            pm.Amount = amount;

            return pm;
        }

        public static OrderModel GetOrderFromSqliteRecord(SQLiteDataReader r)
        {
            int id = Convert.ToInt32(r["Id"]);
            int userid = Convert.ToInt32(r["UserId"]);
            string date = Convert.ToString(r["Date"]);
            string street = Convert.ToString(r["Street"]);
            int house = Convert.ToInt32(r["House"]);
            int apartment = Convert.ToInt32(r["Apartment"]);
            string city = Convert.ToString(r["City"]);
            string country = Convert.ToString(r["Country"]);
            string name = Convert.ToString(r["Firstname"]);
            string lastname = Convert.ToString(r["Lastname"]);
            int cost = Convert.ToInt32(r["Cost"]);

            OrderModel neworder = new OrderModel();
            neworder.Id = id;
            neworder.UserId = userid;
            DateTime newdate = DateTime.Parse(date);
            neworder.OrderDate = newdate;
            neworder.Street = street;
            neworder.House = house;
            neworder.Apartment = apartment;
            neworder.City = city;
            neworder.Country = country;
            neworder.FirstName = name;
            neworder.LastName = lastname;
            neworder.Cost = cost;
            return neworder;

        }

        public static List<Object> GetList(SQLiteDataReader r)
        {
            List<Object> list = new List<Object>();
            string name = Convert.ToString(r["Name"]);
            string price = Convert.ToString(r["Price"]);
            string amount = Convert.ToString(r["Amount"]);

            list.Add(name);
            list.Add(price);
            list.Add(amount);

            return list;
        }
    }
}
