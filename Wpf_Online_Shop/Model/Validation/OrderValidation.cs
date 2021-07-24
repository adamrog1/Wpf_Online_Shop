using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    class OrderValidation
    {
        private static bool CheckPostcode(string postcode, string country)
        {
            string pattern;
            Regex regex;
            postcode = Regex.Replace(postcode, @"\s+", "");
            if (country.Equals("Polska"))
            {
                if (postcode.Length != 6) return false;
                pattern = "[0-9]{2}-[0-9]{3}";
                regex = new Regex(pattern);
                if (!regex.IsMatch(postcode))
                {
                    return false;
                }
                return true;
            }
            else if (country.Equals("Niemcy") || country.Equals("Słowacja"))
            {
                if (postcode.Length != 5) return false;
                pattern = "[0-9]{5}";
                regex = new Regex(pattern);
                if (!regex.IsMatch(postcode))
                {
                    return false;
                }
                return true;
            }
            else if (country.Equals("Czechy"))
            {
                if (postcode.Length != 5) return false;
                pattern = "[1-9]{1}[0-9]{4}";
                regex = new Regex(pattern);
                if (!regex.IsMatch(postcode))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int CheckOrder(OrderModel ord)
        {
            try
            {
                if (ord is null || ord.FirstName is null || ord.LastName is null || ord.City is null || ord.Street is null || ord.House <=0 || ord.Apartment < 0 || ord.Country is null || ord.Postcode is null || ord.Cost<= 0)
                {
                    return 1;
                }
                if (ord.FirstName.Length < 3 || ord.FirstName.Length > 30 || ord.LastName.Length < 3 || ord.LastName.Length > 30)
                {
                    return 3;
                }
                string[] FnameWords = ord.FirstName.Split(' ');
                foreach (var word in FnameWords)
                {
                    if (!word.All(char.IsLetter))
                    {
                        return 2;
                    }
                }
                string[] LnameWords = ord.LastName.Split(' ');
                foreach (var word in LnameWords)
                {
                    if (!word.All(char.IsLetter))
                    {
                        return 2;
                    }
                }

                string[] StreetWords = ord.Street.Split(' ');
                foreach (var word in StreetWords)
                {
                    if (!word.All(char.IsLetterOrDigit))
                    {
                        return 4;
                    }
                }
                if (!CheckPostcode(ord.Postcode,ord.Country))
                {
                    return 5;
                }
                if (ord.House > 100000 || ord.Apartment > 100000)
                {
                    return 6;
                }
                string[] CityWords = ord.City.Split(' ');
                foreach (var word in CityWords)
                {
                    if (!word.All(char.IsLetter))
                    {
                        return 7;
                    }
                }
                    

            }
            catch (ArgumentNullException)
            {
                return 8;
            }
            catch
            {
                return -1;
            }
            return 0;
        }
    }
}
