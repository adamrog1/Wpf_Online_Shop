using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa opisująca możliwe do filtrowania kategorie produktów
    /// </summary>
    public static class ProductCategories
    {
        /// <summary>
        /// Pobiera kategorie produktów
        /// </summary>
        /// <returns>string[]</returns>
        public static string[] Get()
        {
            return new string[5] { "Owoce i warzywa", "Nabiał", "Pieczywo","Mięso","Napoje" };
        }
    }
}
