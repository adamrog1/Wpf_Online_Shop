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

            UserModel um = new UserModel();
            um.Id = id;
            um.Login = login;
            um.Email = email;
            um.Description = desc;
            um.FirstName = firstname;
            um.LastName = lastname;
            um.Password = null;
            return um;
        }

        public static ProductModel GetProductFromSqliteRecord(SQLiteDataReader r)
        {
            int id = Convert.ToInt32(r["Id"]);
            //todo
            return new ProductModel();
        }
    }
}
