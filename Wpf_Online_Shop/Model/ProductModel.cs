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

        public float Price
        {
            get
            {
                return (float)((PLN * 100 + Grosz) / 100.0);
            }
        }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Address { get; set; }
        public string City { get; set;}
        public string Country { get; set; }
        public ProductModel()
        {

        }
    }
}
