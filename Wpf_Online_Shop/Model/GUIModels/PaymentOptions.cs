using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa opisująca opcje płatności
    /// </summary>
    public static class PaymentOptions
    {
        /// <summary>
        /// Pobiera możliwe opcje płatności
        /// </summary>
        /// <returns>string[]</returns>
        public static string[] Get()
        {
            return new string[1] { "Pobranie z konta" };
        }
    }
}
