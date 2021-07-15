using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Model;

    public class OrderExecuteViewModel : ViewModel
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public int HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        private bool SetNewOrder()
        {
            OrderModel neworder = new OrderModel();
            neworder.ListofProducts = CartContent.CartItemsList;
            neworder.User = CurrentState.LoggedUser;
            return true;
        }
        public OrderExecuteViewModel()
        {

        }
    }
}
