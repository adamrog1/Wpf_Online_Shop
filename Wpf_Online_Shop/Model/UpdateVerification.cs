using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    public class UpdateVerification
    {
        public static bool verify_login(string login)
        {
            if (!login.All(char.IsLetterOrDigit)|| login.Length < 3 || login.Length > 18)
            {
                return false;
            } 
            return true;

        }
        public static bool verify_name(string name)
        {
            if (!name.All(char.IsLetterOrDigit) || name.Length < 3 || name.Length > 30)
            {
                return false;
            }
            return true;

        }
        public static bool verify_lastname(string lastname)
        {
            if (lastname.Length < 3 || lastname.Length > 30 || !lastname.All(char.IsLetterOrDigit))
            {
                return false;
            }
            return true;

        }
        public static bool verify_email(string email)
        {
            if (!IsValidEmail(email))
            {
                return false;
            }
            return true;

        }

        public static bool verify_password(string password)
        {
            if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit) || password.Length <= 7 || password.Length > 25)
            {
                return false;
            }
            return true;

        }         
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
