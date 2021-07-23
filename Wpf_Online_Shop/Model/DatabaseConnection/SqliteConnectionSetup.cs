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
        /// <summary>
        /// Pobiera z konfiguracji connectionString do bazy sqlite
        /// </summary>
        /// <param name="name"></param>
        /// <returns>string</returns>
        public static string LoadConnectionString(string name = "Main")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
