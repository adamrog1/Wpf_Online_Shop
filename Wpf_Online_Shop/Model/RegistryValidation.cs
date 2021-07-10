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
        public static bool checkNewUser(string name, string surname, string email, string password, string second_password)
        {
            //null check
            try
            {
                //name and surname check
                if (name.Equals("some name in database") && surname.Equals("some surname in database"))
                {
                    MessageBox.Show("Użytkownik o podanym imieniu i nazwisku już istnieje");
                    return false;
                }
                //second password chcek
                if (password != second_password)
                {
                    MessageBox.Show("Powtórzone hasło nie zgadza się z pierwszym");
                    return false;
                }
                //database password check
                if (password.Equals("some password that's already in database") || !password.Any(char.IsUpper) || !password.Any(char.IsDigit)|| password.Length<=7)
                {
                    MessageBox.Show("Podane hasło już istnieje lub nie spełnia wymagań");
                    return false;
                }
                //email check
                if (email.Equals("some e-mail in database"))
                {
                    MessageBox.Show("Użytwkonik o podanym mailu już istnieje");
                    return false;
                }
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Podany e-mail jest nieprawidłowy");
                    return false;
                }

            }catch(NullReferenceException)
            {
                MessageBox.Show("Wszystkie pola muszą zostać wypełnione");
                return false;
            }
            return true;
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
