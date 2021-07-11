using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public static class ProductCategories
    {
        public static string[] Get()
        {
            return new string[3] { "Owoce i warzywa", "Nabiał", "Pieczywo" };
        }
    }
}
