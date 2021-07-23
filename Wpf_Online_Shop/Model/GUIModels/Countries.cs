using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public static class Countries
    {
        /// <summary>
        /// Pobiera kategorie produktów
        /// </summary>
        /// <returns>string[]</returns>
        public static string[] Get()
        {
            return new string[4] { "Czechy", "Niemcy", "Polska", "Słowacja" };
        }
    }
}
