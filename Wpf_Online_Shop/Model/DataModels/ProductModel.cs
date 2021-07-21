using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa opisująca produkt możliwy do zamówienia
    /// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public int PLN { get; set; }
        public int Grosz { get; set; }

        /// <summary>
        /// Zwraca tekst pokazujący cenę w formacie "ab,cd zł"
        /// </summary>
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

        /// <summary>
        /// Cena produktu w groszach
        /// </summary>
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

        /// <summary>
        /// Metoda sprawdzająca, czy możliwe jest pobranie określonej ilości towarów z magazynu
        /// </summary>
        /// <param name="cartAmount"></param>
        /// <returns></returns>
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
