using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa opisuj¹ca u¿ytkownika aplikacji
    /// </summary>
    public class UserModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone { get; set; }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private int cash;
        /// <summary>
        /// Stan konta u¿ytkownika w groszach
        /// </summary>
        public int Cash
        {
            get { return cash; }
            set { cash = value; }
        }
        /// <summary>
        /// Przekszta³ca stan konta w groszach na zapis tekstowy
        /// </summary>
        public string GetCashText
        {
            get
            {
                int groszcount = this.Cash;
                if (groszcount == 0) return "0 z³";
                StringBuilder result = new StringBuilder(groszcount.ToString());
                if (groszcount < 100) result.Insert(0, "0");
                if (groszcount < 10) result.Insert(0, "0");
                result.Insert(result.Length - 2, ",");
                result.Append(" z³");

                return result.ToString();
            }
        }

        public UserModel()
        {

        }

        public override string ToString()
        {
            return "Id: " + Id + " Login: " + Login + " Email:" + Email + " Opis: " + Description;
        }
    }
}