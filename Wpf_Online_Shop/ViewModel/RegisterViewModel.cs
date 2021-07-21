using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Wpf_Online_Shop.Model;

    public class RegisterViewModel : ViewModel
    {
        #region pola formularza
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
        #endregion
        /// <summary>
        /// Sprawdzenie popranwości formularza
        /// </summary>
        /// <returns></returns>
        private bool checkFormValid()
        {
            try
            {
                if (this.Login is null || this.Name is null || this.Surname is null || this.Password is null || this.SecondPassword is null || this.Email is null)
                {
                    MessageBox.Show("Wszystkie pola muszą zostać wypełnione");
                    return false;
                }
                int a = RegistryValidation.checkNewUser(this.Name, this.Surname, this.Email.ToLower(), this.Password, this.SecondPassword, this.Login.ToLower());
                bool status = true;
                if (a == 0) { return status; }
                else if (a == 1) { MessageBox.Show("Powtórzone hasło nie zgadza się z pierwszym"); status = false; }
                else if (a == 2) { MessageBox.Show("Podane hasło nie spełnia wymagań"); status = false; }
                else if (a == 3) { MessageBox.Show("Email musi miec odpowiedni format"); status = false; }
                else if (a == 4) { MessageBox.Show("Login nie może posiadać znaków specjalnych"); status = false; }
                else if (a == 5) { MessageBox.Show("Login musi być długości od 3 do 18 znaków."); status = false; }
                else if (a == 6) { MessageBox.Show("Imię i nazwisko nie mogą zawierać znaków specjalnych."); status = false; }
                else if (a == 7) { MessageBox.Show("Imię lub/i nazwisko nie są odpowiedniej długości (3-30)"); status = false; }
                else if (a == 8) { MessageBox.Show("Wszystkie pola muszą zostać wypełnione"); status = false; }

                else if (a == -1) { MessageBox.Show("Błąd walidacji."); status = false; }
                return status;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public event EventHandler<EventArgs> UserRegisteredEvent;

        public ICommand registerCommand;
        /// <summary>
        /// Komenda obsługująca wysłanie formularza do rejestracji użytkownika
        /// </summary>
        public ICommand RegisterCommand
        {
            get
            {
                return registerCommand ?? (registerCommand = new RelayCommand(
                    (p) => {
                        var values = (object[])p;
                        PasswordBox p1 = values[0] as PasswordBox;
                        PasswordBox p2 = values[1] as PasswordBox;
                        this.Password = p1.Password.ToString();
                        this.SecondPassword = p2.Password.ToString();
                        if (checkFormValid())
                        {
                            UserModel newUser = new UserModel();
                            newUser.Login = this.Login.ToLower();
                            newUser.Password = this.Password;
                            newUser.Email = this.Email.ToLower();
                            newUser.FirstName = this.Name;
                            newUser.LastName = this.Surname;
                            try
                            {
                                if(Model.DatabaseConnection.SqliteInsert.RegisterUser(newUser))
                                {
                                    MessageBox.Show("Zostałeś zarejestrowany! Możesz się już zalogować.");
                                    UserRegisteredEvent?.Invoke(this,EventArgs.Empty);
                                }
                                else
                                {
                                    MessageBox.Show("Błąd przy rejestracji.");
                                }
                            }
                            catch(Exception e)
                            {
                                if (e.Message.Substring(e.Message.Length - 5).Equals("Login"))
                                {
                                    MessageBox.Show("Podany login już istnieje.");
                                }
                                else if (e.Message.Substring(e.Message.Length - 5).Equals("Email"))
                                {
                                    MessageBox.Show("Istnieje użytkownik o podanym adresie e-mail");
                                }
                                else
                                {
                                    MessageBox.Show("Błąd przy rejestracji. " + e.Message);
                                }
                            }
                        }

                    }, p => true));
            }
        }
        public RegisterViewModel()
        {

        }
    }
}