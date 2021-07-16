using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public static class CartContent
    {
        private static List<CartItemModel> cartItemsList = new List<CartItemModel>();

        public static List<CartItemModel> CartItemsList
        {
            get { return cartItemsList; }
            set { cartItemsList = value; }
        }

        public static CartItemModel GetExistingItemById(int id)
        {
            CartItemModel cartItem = null;
            if (CartItemsList.Count <= 0) return null;
            CartItemsList.ForEach(item =>
            { 
                if(item.Product.Id == id)
                {
                    cartItem = item;
                }
            });
            return cartItem;
        }

        public static void RemoveItemFromCart(CartItemModel itemToRemove)
        {
            if (itemToRemove is null) return;
            CartItemsList.Remove(itemToRemove);
        }

        public static int GetCartItemsCost
        {
            get
            {
                if (CartItemsList.Count == 0) return 0;
                int sum = 0;
                CartItemsList.ForEach(item =>
                {
                    sum += item.SumOfGrosz();
                });
                return sum;
            }
        }

        public static string GetCartItemsCostText
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
        }
    }
}
