using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model.DataModels
{
    public class OrderProductsModel
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Adress { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public string GetPrice
        {
            get
            {
                int groszcount = this.Price;
                if (groszcount == 0) return "0 zł";
                StringBuilder result = new StringBuilder(groszcount.ToString());
                if (groszcount < 100) result.Insert(0, "0");
                if (groszcount < 10) result.Insert(0, "0");
                result.Insert(result.Length - 2, ",");
                result.Append(" zł");

                return result.ToString();
            }
        }
    }
}
