using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Model;

    public class LoginViewModel : ViewModel
    {
        private string tekscik;

        public string Tekscik
        {
            get { return tekscik; }
            set { tekscik = value; }
        }

        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public void OnLoginSuccess(object sender, EventArgs e)
        {
            //LoginChangeView?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<Templates.LoginData> LoginChangeView;

        public LoginViewModel()
        {
            Tekscik = "siema";
        }



        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(
                    (p) => {
                        Templates.LoginData args = new Templates.LoginData();
                        var passbox = p as PasswordBox;
                        var password = passbox.Password;
                        UserModel user = Model.DatabaseConnection.SqliteSelect.GetUserByLogin(this.Login, password);
                        args.UserModel = user;
                        if (user is null)
                        {
                            MessageBox.Show("Nieprawidłowy login lub/i hasło.");
                        }
                        else if (CurrentState.LoggedUser != null && CurrentState.LoggedUser.Login == user.Login)
                        {
                            MessageBox.Show("Jesteś już zalogowany.");
                        }
                        else
                        {
                            MessageBox.Show("Zalogowano jako: " + this.Login);
                            LoginChangeView?.Invoke(this, args);
                        }
                    }, p => true));
            }
            set
            {

            }
        }





    }

    
}
