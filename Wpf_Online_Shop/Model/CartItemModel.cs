using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public class CartItemModel
    {
        public CartItemModel(ProductModel product,int amount)
        {
            Product = product;
            if (amount < 0) amount = 0;
            CartAmount = amount;
        }
        public ProductModel Product { get; private set; }
        public int CartAmount { get; private set; }

        public bool CartAmountIncrease(int addition)
        {
            if (Product.CheckAmount(CartAmount+addition))
            {
                CartAmount = CartAmount + addition;
                return true;
            }
            return false;
        }

        public int SumOfGrosz()
        {
            return Product.PriceGrosz * CartAmount;
        }

        public string GetItemPriceString()
        {
            int groszcount = this.SumOfGrosz();
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
