using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa opisująca przedmioty w koszyku do wysyłki
    /// </summary>
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

        /// <summary>
        /// Zwiększa ilość danego produktu, który już był w koszyku w pewnej ilości
        /// </summary>
        /// <param name="addition"></param>
        /// <returns></returns>
        public bool CartAmountIncrease(int addition)
        {
            if (Product.CheckAmount(CartAmount+addition))
            {
                CartAmount = CartAmount + addition;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Całkowita wartość elementu w koszyku (cena produktu * ilość danego produktu w koszyku)
        /// </summary>
        /// <returns></returns>
        public int SumOfGrosz()
        {
            return Product.PriceGrosz * CartAmount;
        }

        /// <summary>
        /// Zwraca tekst pokazujący cenę w formacie "ab,cd zł"
        /// </summary>
        public string GetItemPriceString
        {
            get
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
}
