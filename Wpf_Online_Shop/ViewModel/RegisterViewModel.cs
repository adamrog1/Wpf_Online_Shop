using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;
    using Wpf_Online_Shop.Model;

    public class RegisterViewModel : ViewModel
    {
        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string passwd;

        public string Password
        {
            get { return passwd; }
            set { passwd = value; }
        }

        private string secondpasswd;

        public string SecondPassword
        {
            get { return secondpasswd; }
            set { secondpasswd = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string name;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public ICommand registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                return registerCommand ?? (registerCommand = new RelayCommand(
                    (p) => {
                        if(RegistryValidation.checkNewUser(this.Name, this.Surname, this.Email, this.Password, this.SecondPassword))
                        {
                            UserModel newUser = new UserModel();
                            // no i tutaj stworzenie użytwkonika do bazy, domyślam się że to będzie coś w stylu jak w loginViewModel czyli
                            // newuser = Model.DatabaseConnection.SqliteSelect.CreateUser(parametry);

                        }

                    }, p => true));
            }
        }
        public RegisterViewModel()
        {

        }
    }
}