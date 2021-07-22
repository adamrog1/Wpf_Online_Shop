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
        public string Manufacurer { get; set; }
        public string Adress { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
