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
        public RegisterViewModel registerVM;
        public ProductsViewModel productsVM;
        public CartViewModel cartVM;
        public ProfileViewModel profileVM;
        public OrderExecuteViewModel orderExecuteVM;

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


        public UserModel LoggedUser
        {
            get
            {
                return CurrentState.LoggedUser;
            }
            set
            { 
                CurrentState.LoggedUser = value;
                onPropertyChange(nameof(LoggedUser));
                onPropertyChange(nameof(LoggedUserString));
            }
        }

        public string LoggedUserString { get { if (LoggedUser is null) return "nie zalogowano"; else return LoggedUser.Login; } set { }}

        public ICommand SwitchViewCommand { get; set; }

        public MainViewModel()
        {
            loginVM = new LoginViewModel();
            registerVM = new RegisterViewModel();
            productsVM = new ProductsViewModel();
            cartVM = new CartViewModel();
            profileVM = new ProfileViewModel();

            homeVM = new HomeViewModel();
            selectedViewModel = homeVM;

            SwitchViewCommand = new Commands.SwitchViewCommand(this);
            loginVM.LoginChangeView += OnLoginSuccess;
            cartVM.CartConfirmedEvent += OnCartConfirmed;
            registerVM.UserRegisteredEvent += OnUserRegistered;
            profileVM.LogoutEvent += OnUserLogout;
        }

        private void OnLoginSuccess(object sender, Templates.LoginData args)
        {
            LoggedUser = args.UserModel;
            SelectedViewModel = homeVM;
        }

        private void OnUserRegistered(object sender, EventArgs e)
        {
            registerVM = new RegisterViewModel();
            registerVM.UserRegisteredEvent += OnUserRegistered;
            SelectedViewModel = homeVM;
        }

        private void OnCartConfirmed(object sender, EventArgs e)
        {
            orderExecuteVM = new OrderExecuteViewModel();
            orderExecuteVM.OrderDoneEvent += OnOrderDone;
            SelectedViewModel = orderExecuteVM;
        }

        private void OnOrderDone(object sender, EventArgs e)
        {
            CartContent.CartItemsList = new List<CartItemModel>(); //Remove all items from the cart
            productsVM = new ProductsViewModel(); //Refresh Products Viewmodel so it has refreshed list of products
            SelectedViewModel = homeVM; //After correct execution come back to home
        }

        private void OnUserLogout(object sender, EventArgs e)
        {
            LoggedUser = null;
            SelectedViewModel = homeVM;
        }
    }
}
