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
    using Model;

    public class CartViewModel : ViewModel
    {
        public List<CartItemModel> CartItemsList
        {
            get
            {
                return CartContent.CartItemsList;
            }
            private set
            {

            }
        }

        public int ItemsCostSum
        {
            get
            {
                return CartContent.GetCartItemsCost;
            }
            set
            { }
        }

        public string ItemsCostSumString
        {
            get
            {
                int groszcount = CartContent.GetCartItemsCost;
                if (groszcount == 0) return "0 zł";
                StringBuilder result = new StringBuilder(groszcount.ToString());
                if (groszcount < 100) result.Insert(0, "0");
                if (groszcount < 10) result.Insert(0, "0");
                result.Insert(result.Length - 2, ",");
                result.Append(" zł");

                return result.ToString();
            }
            private set
            {
            }
        }


        public CartViewModel()
        {

        }
    }
}
