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
        public static int CheckOrder(OrderModel ord)
        {
            try
            {
                if (ord is null || ord.FirstName is null || ord.LastName is null || ord.City is null || ord.Street is null || ord.House <=0 || ord.Apartment is null || ord.Apartment <= 0 || ord.Country is null || ord.Postcode is null || ord.Cost<= 0)
                {
                    return 1;
                }
                if (ord.FirstName.Length < 3 || ord.FirstName.Length > 30 || ord.LastName.Length < 3 || ord.LastName.Length > 30)
                {
                    return 3;
                }
                string FnameNoMultipleSpaces = Regex.Replace(ord.FirstName, @"\s+", " ");
                string[] FnameWords = FnameNoMultipleSpaces.Split(' ');
                foreach (var word in FnameWords)
                {
                    if (!word.All(char.IsLetter))
                    {
                        return 2;
                    }
                }
                string LnameNoMultipleSpaces = Regex.Replace(ord.LastName, @"\s+", " ");
                string[] LnameWords = LnameNoMultipleSpaces.Split(' ');
                foreach (var word in LnameWords)
                {
                    if (!word.All(char.IsLetter))
                    {
                        return 2;
                    }
                }

                string StreetNoMultipleSpaces = Regex.Replace(ord.Street, @"\s+", " ");
                string[] StreetWords = StreetNoMultipleSpaces.Split(' ');
                foreach (var word in StreetWords)
                {
                    if (!word.All(char.IsLetterOrDigit))
                    {
                        return 4;
                    }
                }
                string pattern = "[0-9]*-[0-9]*";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(ord.Postcode))
                {
                    return 5;
                }
                if (ord.House > 100000 || ord.Apartment > 100000)
                {
                    return 6;
                }
                string CityNoMultipleSpaces = Regex.Replace(ord.City, @"\s+", " ");
                string[] CityWords = CityNoMultipleSpaces.Split(' ');
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
