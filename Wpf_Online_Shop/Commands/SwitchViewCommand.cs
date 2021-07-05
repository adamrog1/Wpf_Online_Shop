using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Online_Shop.ViewModel;
using Wpf_Online_Shop.View;
using System.Windows.Input;
using Wpf_Online_Shop.Model;

namespace Wpf_Online_Shop.Commands
{
    public class SwitchViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public SwitchViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Home")
            {
                viewModel.SelectedViewModel = new HomeViewModel();
                return;
            }
            if (parameter.ToString() == "Login")
            {
                this.viewModel.loginVM.Tekscik = "zmieniony tekst";
                viewModel.SelectedViewModel = this.viewModel.loginVM;
                return;
            }
            if (parameter.ToString() == "Register")
            {
                viewModel.SelectedViewModel = this.viewModel.registerVM;
                return;
            }
            if (parameter.ToString() == "Products")
            {
                viewModel.SelectedViewModel = this.viewModel.productsVM;
                return;
            }
            if (parameter.ToString() == "Cart")
            {
                viewModel.SelectedViewModel = this.viewModel.cartVM;
                return;
            }
            if (parameter.ToString() == "Profile")
            {
                viewModel.SelectedViewModel = this.viewModel.profileVM;
                return;
            }
        }
    }
}
