using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Online_Shop.View;
using Wpf_Online_Shop.Model;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;

    public class MainViewModel : BaseClass.ViewModel
    {
        private ViewModel selectedViewModel;

        public LoginViewModel loginVM;
        public HomeViewModel homeVM;

        public ViewModel SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }
            set
            {
                selectedViewModel = value;
                onPropertyChange(nameof(selectedViewModel));
            }
        }

        private string loggedUser;

        public string LoggedUser
        {
            get { return CurrentState.LoggedUser; }
            set
            { 
                CurrentState.LoggedUser = value;
                onPropertyChange(nameof(loggedUser));
            }
        }

        public void OnLoginSuccess(object sender, EventArgs e)
        {
            SelectedViewModel = homeVM;
        }

        public ICommand SwitchViewCommand { get; set; }

        public MainViewModel()
        {
            loginVM = new LoginViewModel();
            homeVM = new HomeViewModel();
            selectedViewModel = loginVM;

            SwitchViewCommand = new Commands.SwitchViewCommand(this);
            loginVM.LoginChangeView += OnLoginSuccess;
        }

    }
}
