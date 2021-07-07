using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Online_Shop.Model;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;

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

        private ICommand registerCom;
        public ICommand RegisterCom
        {
            get
            {
                return registerCom ?? (registerCom = new RelayCommand(
                    (p) => {
                        UserModel user = new UserModel();
                        user.Login = "adrian1";
                        user.Password = "szafagra";
                        user.Email = "adrian@onet.pl";
                        user.FirstName = "Adrian";
                        user.LastName = "Rekin";
                        try
                        {
                            if (Model.DatabaseConnection.SqliteInsert.RegisterUser(user))
                            {
                                MessageBox.Show("Zarejestrowano poprawnie!");
                            }
                            else
                            {
                                MessageBox.Show("Błąd");
                            }
                        }
                        catch(System.Data.SQLite.SQLiteException e)
                        {
                            if (e.ErrorCode == 19)
                            {
                                MessageBox.Show("Istnieje już użytkownik o podanym adresie mailowym lub/i loginie.");
                            }
                            else if (e.ErrorCode == 1)
                            {
                                MessageBox.Show("Nie można połączyć się z bazą danych");
                            }
                            else
                            {
                                MessageBox.Show(e.Message);
                            }
                        }
                    }, p => true));
            }
            set
            {

            }
        }

        public RegisterViewModel()
        {

        }
    }
}