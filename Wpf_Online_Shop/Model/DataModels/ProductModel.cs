using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public int PLN { get; set; }
        public int Grosz { get; set; }

        public string GetPriceText
        {
            get
            {
                StringBuilder result = new StringBuilder(PLN.ToString(), 12);
                result.Append(',');
                if (Grosz <= 9) result.Append('0');
                result.Append(Grosz.ToString());
                result.Append(" zł");
                return result.ToString();
            }
        }

        public string GetManufacturerLocation
        { 
            get
            {
                return Address + ", " + City;
            }
        }

        public int PriceGrosz
        {
            get
            {
                return PLN * 100 + Grosz;
            }
        }

        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Address { get; set; }
        public string City { get; set;}
        public string Country { get; set; }

        public int Amount { get; set; }

        public bool CheckAmount(int cartAmount)
        {
            if (Amount - cartAmount < 0) return false;
            return true;
        }

        public ProductModel()
        {

        }
    }
}
