using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public static class PaymentOptions
    {
        public static string[] Get()
        {
            return new string[1] { "Pobranie z konta" };
        }
    }
}
