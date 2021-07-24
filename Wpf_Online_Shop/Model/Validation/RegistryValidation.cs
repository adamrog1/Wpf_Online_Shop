using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa dotycz¹ca walidacji formularza rejestrowania siê
    /// </summary>
    class RegistryValidation
    {
        /// <summary>
        /// Sprawdzenie poprawnoœci danych rejestracji u¿ytkownika
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="second_password"></param>
        /// <param name="login"></param>
        /// <returns></returns>
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
                if (login.Length<3 || login.Length>18)
                {
                    return 5;
                }
                if (!name.All(char.IsLetterOrDigit) || !surname.All(char.IsLetterOrDigit))
                {
                    return 6;
                }
                if (name.Length<3 || name.Length>30 || surname.Length<3 ||surname.Length>30)
                {
                    return 7;
                }

            }catch(ArgumentNullException)
            {
                return 8;
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