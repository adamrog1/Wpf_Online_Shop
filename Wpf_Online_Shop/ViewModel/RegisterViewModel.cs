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
                        int a=RegistryValidation.checkNewUser(this.Name, this.Surname, this.Email, this.Password, this.SecondPassword, this.Login);
                        if (a == 0) MessageBox.Show("Stworzono użytwkonika");
                        else if (a == 1) MessageBox.Show("Powtórzone hasło nie zgadza się z pierwszym");
                        else if (a == 2) MessageBox.Show("Podane hasło już istnieje lub nie spełnia wymagań");
                        else if (a == 3) MessageBox.Show("Email musi miec odpowiedni format");
                        else if (a == 4) MessageBox.Show("Login nie może posiadać znaków specjalnych");
                        else if (a == 5) MessageBox.Show("Wszystkie pola muszą zostać wypełnione");

                    }, p => true));
            }
        }
        public RegisterViewModel()
        {

        }
    }
}