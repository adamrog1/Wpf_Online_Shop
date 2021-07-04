using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
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
            LoginChangeView?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler LoginChangeView;
    
        public LoginViewModel()
        {
            Tekscik = "siema";
            LoginCommand = new Commands.LoginCommand(this);
            LoginCommand.CanExecuteChanged += OnLoginSuccess;
        }

        public ICommand LoginCommand { get; set; }





    }
}
