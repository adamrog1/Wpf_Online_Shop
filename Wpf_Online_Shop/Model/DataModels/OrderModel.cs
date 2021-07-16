using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public class OrderModel
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        private List<CartItemModel> listofproducts=new List<CartItemModel>();
        public List<CartItemModel> ListofProducts
        {
            get { return listofproducts; }
            set { listofproducts = value; }
        }

        public string Street { get; set; }

        public int House { get; set; }

        public int? Apartment { get; set; } = null;

        public string Postcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Cost { get; set; }

        public string GetDateAsText
        {
            get { return OrderDate.ToString(); }
        }

        public string GetCartItemsCostText
        {
            get
            {
                int groszcount = this.Cost;
                if (groszcount == 0) return "0 zł";
                StringBuilder result = new StringBuilder(groszcount.ToString());
                if (groszcount < 100) result.Insert(0, "0");
                if (groszcount < 10) result.Insert(0, "0");
                result.Insert(result.Length - 2, ",");
                result.Append(" zł");

                return result.ToString();
            }
        }

        public OrderModel()
        {
            
        }

    }
}
