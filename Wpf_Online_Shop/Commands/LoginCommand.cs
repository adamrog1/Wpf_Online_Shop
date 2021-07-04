using System;
using System.Windows;
using System.Windows.Input;
using Wpf_Online_Shop.ViewModel;
using Wpf_Online_Shop.Model;

namespace Wpf_Online_Shop.Commands
{
    internal class LoginCommand : ICommand
    {
        private LoginViewModel loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        //public event EventHandler LoginSuccess;

        public void Execute(object parameter)
        {
            string login = this.loginViewModel.Login;
            MessageBox.Show(login);
            CurrentState.LoggedUser = login;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}