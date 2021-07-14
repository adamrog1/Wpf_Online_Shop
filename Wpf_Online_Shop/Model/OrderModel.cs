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
        private int order_id;
        public int Order_id {
            get { return order_id; }
            set { order_id = value; }
        }

        private DateTime orderdate;
        public DateTime OrderDate
        {
            get { return orderdate; }
            set { orderdate = value; }
        }

        private ObservableCollection<CartItemModel> listofproducts=new ObservableCollection<CartItemModel>();
        public ObservableCollection<CartItemModel> ListofProducts
        {
            get { return listofproducts; }
            set { listofproducts = value; }
        }

        private string userorderdata;
        public string UserOrderData
        {
            get { return userorderdata; }
            set { userorderdata = value; }
        }

        private UserModel customer;
        public UserModel Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        private string useradress;
        public string UserAdres
        {
            get { return useradress; }
            set { useradress = value; }
        }

        public string Country{ get; set; }
        public string City {get; set; }
        public string PostCode { get; set; }

        public DateTime getcurrentdate()
        {
            return orderdate = DateTime.Now;
        }

        public string ConverDatetoString()
        {
            return orderdate.ToString();
        }

    }
}
