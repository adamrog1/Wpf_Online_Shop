using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Online_Shop.Model
{
    class RegistryValidation
    {
        public static int checkNewUser(string name, string surname, string email, string password, string second_password,string login)
        {
            //null check
            try
            {
                //second password chcek
                if (password != second_password)
                {
                    return 1;
                }
                // password check
                if ( !password.Any(char.IsUpper) || !password.Any(char.IsDigit)|| password.Length<=7 ||password.Length>25)
                {
                    return 2;
                }
              
                if (!IsValidEmail(email))
                {
                    return 3;
                }
                //login check; can't have special char
                if (!login.All(char.IsLetterOrDigit))
                {
                    return 4;
                }

            }catch(ArgumentNullException)
            {
                return 5;
            }
            return 0;
        }
        //email validation method
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}