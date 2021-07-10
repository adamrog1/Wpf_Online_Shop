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
            try
            {
                if (name is null || surname is null || email is null || password is null || second_password is null || login is null)
                {
                    return -1;
                }
                //name and surname check
                if (name.Equals("some name in database") && surname.Equals("some surname in database"))
                {
                    return 1;
                }
                //second password check
                if (password != second_password)
                {
                    return 2;
                }
                //database password check
                if (password.Equals("some password that's already in database") || !password.Any(char.IsUpper) || !password.Any(char.IsDigit) || password.Length <= 7 || password.Length > 25)
                {
                    return 3;
                }
                //email check
                if (email.Equals("some e-mail in database"))
                {
                    return 4;
                }
                if (!IsValidEmail(email))
                {
                    return 5;
                }
                //login check; can't have special char
                if (!login.All(char.IsLetterOrDigit))
                {
                    return 6;
                }
            }
            catch
            {
                return -1;
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
