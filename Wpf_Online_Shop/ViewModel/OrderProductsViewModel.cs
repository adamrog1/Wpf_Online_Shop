using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Wpf_Online_Shop.Model;
    using Wpf_Online_Shop.Model.DataModels;

    public class OrderProductsViewModel : ViewModel
    {
        private int? orderid;
        public void setorderid(int? order_id)
        {
            orderid = order_id;
        }
        public List<OrderProductsModel> List
        {
            get { return Model.DatabaseConnection.SqliteSelect.GetOrderProducts(orderid); }
            set {; }
        }
    }
}
