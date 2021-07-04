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
                        args.Login = this.Login;
                        MessageBox.Show("login: "+ this.Login);
                        LoginChangeView?.Invoke(this, args);
                    }, p => true));
            }
            set
            {

            }
        }





    }

    
}
