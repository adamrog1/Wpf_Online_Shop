using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
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

        public UserModel()
        {

        }

        public override string ToString()
        {
            return "Id: " + Id + " Login: " + Login + " Email:" + Email + " Opis: " + Description;
        }
    }
}
