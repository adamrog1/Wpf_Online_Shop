using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Wpf_Online_Shop.Model.DatabaseConnection
{
    static public class SqliteConnectionSetup
    {
        public static string LoadConnectionString(string name = "Main")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
